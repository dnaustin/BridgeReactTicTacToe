using Bridge.React;
using System;

namespace BridgeReactTicTacToe.Components
{
    public class Board : StatelessComponent<Board.Props>
    {
        public Board(Props props) : base(props) { }

        private Square renderSquare(int i)
        {
            return new Square(new Square.Props
            {
                value = this.props.squares[i],
                onClick = () => this.props.onClick(i)
            });
        }

        public override ReactElement Render()
        {
            return DOM.Div(new Attributes { },
                DOM.Div(new Attributes { ClassName = "board-row" },
                    this.renderSquare(0),
                    this.renderSquare(1),
                    this.renderSquare(2)),
                DOM.Div(new Attributes { ClassName = "board-row" },
                    this.renderSquare(3),
                    this.renderSquare(4),
                    this.renderSquare(5)),
                DOM.Div(new Attributes { ClassName = "board-row" },
                    this.renderSquare(6),
                    this.renderSquare(7),
                    this.renderSquare(8))
                );
        }

        public class Props
        {
            public string[] squares;
            public Action<int> onClick;
        }
    }
}
