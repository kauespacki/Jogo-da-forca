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
    public static int QuantidadeTentativas { get; set; }
    public static void Iniciar(Menu menu)
    {
        Console.Clear();
        Console.WriteLine($"Categoria sorteada: {menu.Categoria}");
        Jogar(menu);
    }

    private static void Jogar(Menu menu)
    {
        EscreverPalavra(menu.PalavraSorteada);
        Ganhou(menu.PalavraSorteada);
        ReceberLetra(menu.PalavraSorteada);
        Jogar(menu);
    }

    private static void EscreverPalavra(string palavra)
    {
        Console.Clear();
        Console.Write("Palavra: ");

        for (int i = 0; i < palavra.Length; i++)
        {
            if (LetrasEscolhidas.Exists(x=> x==palavra[i].ToString()))
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

    private static void ReceberLetra(string palavra)
    {
        Console.Write("Letra: ");
        var letra = Console.ReadLine().ToLower().Trim();
        if (LetrasEscolhidas.Exists(x=>x == letra))
        {
            Console.WriteLine("Essa letra já foi escolhida.");
            Console.ReadKey();
            return;
        }

        LetrasEscolhidas.Add(letra);
        AcertouAPalavra(palavra);
        QuantidadeTentativas++;
    }

    private static void Ganhou(string palavra)
    {
        foreach (var letra in palavra)
        {
            if (!LetrasEscolhidas.Exists(x=>x == letra.ToString()))
            {
                return;
            }
        }

        Console.WriteLine($"Parabéns! Você ganhou com {QuantidadeTentativas} tentativas!");
        Environment.Exit(0);
    }

    private static void AcertouAPalavra(string palavra)
    {
        foreach (var letra in palavra)
        {
            if (LetrasEscolhidas.Exists(x=> x == palavra))
            {
                Console.WriteLine($"Parabéns! Você ganhou com {QuantidadeTentativas} tentativas!");
                Environment.Exit(0);
            }
        }
    }
}
