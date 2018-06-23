using Bridge.React;
using System.Linq;

namespace BridgeReactTicTacToe.Components
{
    public class Board : Component<Board.Props, Board.State>
    {
        public Board(Props props) : base(props) { }

        protected override State GetInitialState()
        {
            return new State
            {
                squares = Enumerable.Repeat(string.Empty, 9).ToArray(),
                xIsNext = true
            };
        }

        private Square renderSquare(int i)
        {
            return new Square(new Square.Props
            {
                value = this.state.squares[i],
                onClick = () => this.handleClick(i)
            });
        }

        private void handleClick(int i)
        {
            string[] newSquares = (string[])this.state.squares.Clone();

            if (calculateWinner(newSquares) != string.Empty || newSquares[i] != string.Empty)
            {
                return;
            }

            newSquares[i] = this.state.xIsNext ? "X" : "O";
            this.SetState(new State { squares = newSquares, xIsNext = !this.state.xIsNext });
        }

        private string calculateWinner(string[] squares)
        {
            var lines = new int[8][]
            {
                new int[3] {0, 1, 2},
                new int[3] {3, 4, 5},
                new int[3] {6, 7, 8},
                new int[3] {0, 3, 6},
                new int[3] {1, 4, 7},
                new int[3] {2, 5, 8},
                new int[3] {0, 4, 8},
                new int[3] {2, 4, 6},
            };

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (squares[line[0]] != string.Empty && 
                    squares[line[0]] == squares[line[1]] && 
                    squares[line[0]] == squares[line[2]])
                {
                    return squares[line[0]];
                }
            }

            return string.Empty;
        }

        public override ReactElement Render()
        {
            string winner = this.calculateWinner(this.state.squares);

            string status;

            if (winner != string.Empty)
            {
                status = string.Format("Winner: {0}", winner);
            }
            else
            {
                status = string.Format("Next player: {0}", this.state.xIsNext ? "X" : "O");
            }           

            return DOM.Div(new Attributes { },
                DOM.Div(new Attributes { ClassName = "status" }, status),
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
        }

        public class State
        {
            public string[] squares;
            public bool xIsNext;
        }
    }
}
