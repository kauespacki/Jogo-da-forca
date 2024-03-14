using JogoForca.MenuContexto.Enums;

namespace JogoForca.MenuContexto;

public class Menu
{
    public Menu(int quantidadePalavras)
    {
        QuantidadePalavras = quantidadePalavras;
    }

    public int QuantidadePalavras { get; }
    public string Categoria { get; }
    //
    public string PalavraSorteada { get; set; }
    
    public void Iniciar()
    {
        Console.Clear();
        var categoria = SortearCategoria();
        Console.WriteLine($"Categoria sorteada: {categoria}");
        Console.WriteLine("----- Recebendo valores -----");
        var valores = ReceberPalavra();
        PalavraSorteada = SortearPalavra(valores);
    }

    private Categoria SortearCategoria()
    {
        var sortear = new Random();
        var categoriaNomes = Enum.GetNames(typeof(Categoria));
        // sorteia de 1 a quantidade de categorias
        var categoriaSorteada = sortear.Next(1, categoriaNomes.Length + 1);
        // recebe o nome do enum da categoria
        var nome = Enum.GetName(typeof(Categoria), categoriaSorteada);
        var deuCertoCategoria = Enum.TryParse(nome, out Categoria categoria);
        return categoria;
    }

    private List<string> ReceberPalavra()
    {
        var valores = new List<string>();
        var contador = 1;
        while (contador <= QuantidadePalavras)
        {
            Console.Write($"{contador}. Valor: ");
            var valor = Console.ReadLine().Trim().ToLower();
            if (valor.Trim().Length <= 1)
            {
                Console.WriteLine("Valor inválido.");
                continue;
            }
            valores.Add(valor);
            contador++;
        }

        return valores;
    }

    private string SortearPalavra(List<string> valores)
    {
        var random = new Random();
        // pega um valor aleatório da lista de x itens
        return valores[random.Next(0, valores.Count)].Trim().ToLower();
    }
}