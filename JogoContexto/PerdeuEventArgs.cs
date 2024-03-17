using JogoForca.MenuContexto;

namespace JogoForca.JogoContexto;

public class PerdeuEventArgs : EventArgs
{
    public PerdeuEventArgs(Menu menu)
    {
        Menu = menu;
    }
    public Menu Menu { get; set; }
}