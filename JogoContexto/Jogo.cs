using System.Text;
using JogoForca.MenuContexto;

namespace JogoForca.JogoContexto;

public class Jogo
{
    public event EventHandler<PerdeuEventArgs> PerdeuEvent;  
    public Jogo(Menu menu)
    {
        LetrasEscolhidas = new List<string>();
        LetrasEscolhidas.Add(" ");
        switch (menu.Dificuldade)
        {
            case Dificuldade.Facil: Tentativas = 7; break;
            case Dificuldade.Normal: Tentativas = 6; break;
            case Dificuldade.Dificil: Tentativas = 5; break;
        }

        PerdeuEvent += OnPerdeuEvent;

    }
    private void OnPerdeuEvent(object? sender, PerdeuEventArgs e)
    {
        Cabecalho(e.Menu);
        Console.WriteLine("Você perdeu! Tentativas disponíveis esgotadas!");
        Console.WriteLine($"A palavra era '{e.Menu.PalavraSorteada}'");
        Environment.Exit(0);
    }
    public List<string> LetrasEscolhidas { get; }
    public int QuantidadeTentativasUsadas { get; set; }
    public int Tentativas { get; set; }

    public void Jogar(Menu menu)
    {
        Cabecalho(menu);
        EscreverPalavra(menu.PalavraSorteada);
        Ganhou(menu.PalavraSorteada);
        var letra = ReceberLetra();
        LetrasEscolhidas.Add(letra);
        var palpiteCorreto = VerificarPalpite(menu.PalavraSorteada, letra);
        if (!palpiteCorreto) QuantidadeTentativasUsadas++;
        Perdeu(menu);
        Jogar(menu);
    }
    private void Cabecalho(Menu menu)
    {
        Console.Clear();
        Console.WriteLine($"----- tentativas restantes: {Tentativas - QuantidadeTentativasUsadas} -----");
        Console.WriteLine($"Modo: {menu.Dificuldade} - {Tentativas} tentativas");
        Console.WriteLine($"Quantidade de letras da palavra: {menu.PalavraSorteada.Replace(" ", "").Length}");
    }
    private void EscreverPalavra(string palavra)
    {
        Console.Write("Palavra: ");

        for (int i = 0; i < palavra.Length; i++)
        {
            var letraSemAcento = TirarAcento(palavra[i].ToString());
            if (LetrasEscolhidas.Exists(x=> x==letraSemAcento))
            {
                Console.Write($"{palavra[i]}");
            }
            else
            {
                Console.Write("_ ");
            }
        }
        Console.Write("\n");
    }
    private string ReceberLetra()
    {
        while (true)
        {
            Console.Write("Letra: ");
            var letra = Console.ReadLine().ToLower().Trim();
            letra = TirarAcento(letra);
            if (!LetrasEscolhidas.Exists(x=>x == letra))
            {
                return letra;
            }
            Console.WriteLine("Essa letra já foi escolhida.");
            Console.ReadKey();
        }
    }
    private void Ganhou(string palavra)
    {
        var palavraSemAcentos = TirarAcento(palavra);
        AcertouPalavra(palavraSemAcentos);
        foreach (var letra in palavraSemAcentos)
        {
            if (!LetrasEscolhidas.Exists(x=>x == letra.ToString()))
                return;
        }
        Console.WriteLine($"Parabéns! Você ganhou com {QuantidadeTentativasUsadas} tentativas!");
        Environment.Exit(0);
    }
    private void AcertouPalavra(string palavra)
    {
        if (!LetrasEscolhidas.Exists(x => x == palavra))
        {
            return;
        }
        Console.WriteLine($"Parabéns! Você ganhou com {QuantidadeTentativasUsadas} tentativas!");
        Environment.Exit(0);
    }
    private void Perdeu(Menu menu)
    {
        if (QuantidadeTentativasUsadas == Tentativas)
        {
            PerdeuEventHandler(new PerdeuEventArgs(menu));
        }
    }
    private bool VerificarPalpite(string palavra, string letra)
    {
        var palpiteCorreto = false;
        var palavraSemAcento = TirarAcento(palavra);
        foreach (var letraDaPalavra in palavraSemAcento)
        {
            if (letraDaPalavra.ToString() == letra || letra == "" || 
                LetrasEscolhidas.Exists(x=>x == palavraSemAcento))
            {
                palpiteCorreto = true;
                break;
            }
        }

        return palpiteCorreto;
    }
    private string TirarAcento(string texto)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var encodingIso = Encoding.GetEncoding("ISO-8859-8");
        var textoBytes = encodingIso.GetBytes(texto);
        var textoSemAcento = Encoding.UTF8.GetString(textoBytes);
        return textoSemAcento;
    }
    private void PerdeuEventHandler(PerdeuEventArgs e)
    {
        PerdeuEvent?.Invoke(this, e);
    }
}
