using Bridge.React;

namespace BridgeReactTicTacToe.Components
{
    public class Game : StatelessComponent<Game.Props>
    {
        public Game(Props props) : base(props) { }

        public override ReactElement Render()
        {
            return DOM.Div(new Attributes { ClassName = "game" },
                DOM.Div(new Attributes { ClassName = "game-board"}, new Board(new Board.Props())),
                DOM.Div(new Attributes { ClassName = "game-info"},
                    DOM.Div(new Attributes { }),
                    DOM.OL(new OListAttributes { }))
                );
        }

        public class Props
        {
        }
    }
}
