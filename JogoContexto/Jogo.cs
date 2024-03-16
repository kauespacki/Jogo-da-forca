using System.Globalization;
using System.Text;
using JogoForca.MenuContexto;

namespace JogoForca.JogoContexto;

public static class Jogo
{
    static Jogo()
    {
        LetrasEscolhidas = new List<string>();
        LetrasEscolhidas.Add(" ");
    }
    
    public static List<string> LetrasEscolhidas { get; }
    public static int QuantidadeTentativasUsadas { get; set; }
    public static int Tentativas { get; set; }
    public static void Iniciar(Menu menu)
    {
        switch (menu.Dificuldade)
        {
            case Dificuldade.Facil: Tentativas = 7; break;
            case Dificuldade.Normal: Tentativas = 6; break;
            case Dificuldade.Dificil: Tentativas = 5; break;
        }
        Console.Clear();
        Console.WriteLine($"Categoria sorteada: {menu.Categoria}");
        Jogar(menu);
    }

    private static void Jogar(Menu menu)
    {
        EscreverPalavra(menu.PalavraSorteada);
        Ganhou(menu.PalavraSorteada);
        var letra = ReceberLetra(menu.PalavraSorteada);
        VerificarPalpite(menu.PalavraSorteada, letra);
        Perdeu();
        Jogar(menu);
    }

    private static void EscreverPalavra(string palavra)
    {
        Console.Clear();
        Console.WriteLine($"----- tentativas restantes: {Tentativas - QuantidadeTentativasUsadas} -----");
        Console.WriteLine($"Quantidade de letras da palavra: {palavra.Replace(" ", "").Length}");
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

    private static string ReceberLetra(string palavra)
    {
        Console.Write("Letra: ");
        var letra = Console.ReadLine().ToLower().Trim();
        letra = TirarAcento(letra);
        if (LetrasEscolhidas.Exists(x=>x == letra))
        {
            Console.WriteLine("Essa letra já foi escolhida.");
            Console.ReadKey();
            return " ";
        }
        LetrasEscolhidas.Add(letra);
        AcertouAPalavra(palavra);
        return letra;
    }

    private static void Ganhou(string palavra)
    {   var palavraNormalized = palavra.Normalize(NormalizationForm.FormD);
        var contador = 0;
        foreach (var letra in palavraNormalized)
        {
            contador++;
            if (CharUnicodeInfo.GetUnicodeCategory(letra) != UnicodeCategory.NonSpacingMark)
            {
                if (!LetrasEscolhidas.Exists(x => x == letra.ToString()))
                {
                    return;
                }
            }
        }

        Console.WriteLine($"Parabéns! Você ganhou com {QuantidadeTentativasUsadas} tentativas!");
        Environment.Exit(0);
    }

    private static void AcertouAPalavra(string palavra) // depois
    {
        var palavraSemAcento = TirarAcento(palavra);
        foreach (var letra in palavraSemAcento)
        {
            if (LetrasEscolhidas.Exists(x=> x == palavraSemAcento))
            {
                Console.WriteLine($"Parabéns! Você ganhou com {QuantidadeTentativasUsadas} tentativas!");
                Environment.Exit(0);
            }
        }
    }

    private static void Perdeu()
    {
        if (QuantidadeTentativasUsadas == Tentativas)
        {
            Console.Clear();
            Console.WriteLine("Você perdeu! Tentativas disponíveis esgotadas!");
            Environment.Exit(0);
        }
    }

    private static void VerificarPalpite(string palavra, string letra)
    {
        var palpiteCorreto = false;
        var palavraNormalized = palavra.Normalize(NormalizationForm.FormD);
        var contador = 0;
        foreach (var letraDaPalavra in palavraNormalized)
        {
            contador++;
            if (CharUnicodeInfo.GetUnicodeCategory(letraDaPalavra) == UnicodeCategory.NonSpacingMark)
            {
                var letraDaPalavraSemAcento = TirarAcento(letraDaPalavra.ToString());
                if (palavraNormalized[contador-1 - 1].ToString() == letraDaPalavraSemAcento)
                {
                    palpiteCorreto = true;
                    break;
                }
            }
            if (letra == letraDaPalavra.ToString())
            {
                palpiteCorreto = true;
                break;
            }
        }

        if (!palpiteCorreto)
        {
            QuantidadeTentativasUsadas++;
        }
    }

    private static string TirarAcento(string texto)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var encodingIso = Encoding.GetEncoding("ISO-8859-8");
        var textoBytes = encodingIso.GetBytes(texto);
        var textoSemAcento = Encoding.UTF8.GetString(textoBytes);
        return textoSemAcento;
    }
}
