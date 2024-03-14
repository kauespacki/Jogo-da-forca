using JogoForca.JogoContexto;
using JogoForca.MenuContexto;

namespace JogoForca;

public static class JogoDaForca
{
    public static void Iniciar(int quantidadePalavras)
    {
        var menu = new Menu(quantidadePalavras);
        menu.Iniciar();
        Jogo.Iniciar(menu);
    }
}