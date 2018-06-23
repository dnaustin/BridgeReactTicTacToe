using Bridge;
using Bridge.Html5;
using Bridge.React;
using BridgeReactTicTacToe.Components;
using System.Linq;

namespace BridgeReactTicTacToe
{
    public class App
    {
        [Ready]
        public static void Main()
        {
            var container = Document.GetElementById("main");
            container.ClassName = string.Join(" ", container.ClassName.Split().Where(c => c != "loading"));

            React.Render(
                new Game(new Game.Props { }),
                container
            );
        }
    }
}
