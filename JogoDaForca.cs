using JogoForca.JogoContexto;
using JogoForca.MenuContexto;

namespace JogoForca;

public static class JogoDaForca
{
    public static void Iniciar(int quantidadePalavras, Dificuldade dificuldade)
    {
        var menu = new Menu(quantidadePalavras, dificuldade);
        menu.Iniciar();
        Jogo.Iniciar(menu);
    }
}