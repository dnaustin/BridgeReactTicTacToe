using System;
using System.Linq;
using Bridge.React;

namespace BridgeReactTicTacToe.Components
{
    public class Game : Component<Game.Props, Game.State>
    {
        public Game(Props props) : base(props) { }

        protected override State GetInitialState()
        {
            var history = new string[][] { Enumerable.Repeat(string.Empty, 9).ToArray() };

            return new State
            {
                history = history,
                stepNumber = 0,
                xIsNext = true
            };
        }

        public override ReactElement Render()
        {
            var history = this.state.history;
            var current = history[this.state.stepNumber];

            string winner = this.calculateWinner(current);

            string status;

            if (winner != string.Empty)
            {
                status = string.Format("Winner: {0}", winner);
            }
            else
            {
                status = string.Format("Next player: {0}", currentPlayer());
            }

            var moves = history.Select((step, move) =>
            {
                var desc = "Go to move #" + move;
                return DOM.Li(new LIAttributes { Key = move },
                    DOM.Button(new ButtonAttributes { OnClick = (e) => { this.jumpTo(move); } }, desc)
                    );
            });

            return DOM.Div(new Attributes { ClassName = "game" },
                DOM.Div(new Attributes { ClassName = "game-board"}, 
                    new Board(new Board.Props { squares = current, onClick = (i) => this.handleClick(i)})),
                DOM.Div(new Attributes { ClassName = "game-info"},
                    DOM.Div(new Attributes { }, status),
                    DOM.OL(new OListAttributes { }, moves))
                );
        }

        private void jumpTo(int move)
        {
            this.SetState(new State
            {
                history = this.state.history,
                stepNumber = move,
                xIsNext = (move % 2) == 0
            });
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

        private string currentPlayer()
        {
            return this.state.xIsNext ? "X" : "O";
        }

        private void handleClick(int i)
        {
            var newHistory = (string[][])this.state.history.Clone();
            newHistory = (string[][])newHistory.Slice(0, this.state.stepNumber + 1);

            var current = newHistory[newHistory.Length - 1];
            var newSquares = (string[])current.Clone();

            if (calculateWinner(newSquares) != string.Empty || newSquares[i] != string.Empty)
            {
                return;
            }
            
            newSquares[i] = currentPlayer();
            newHistory.Push(newSquares);

            var stepNumber = newHistory.Length - 1;

            this.SetState(new State
            {
                history = newHistory,
                stepNumber = stepNumber,
                xIsNext = !this.state.xIsNext
            });
        }

        public class Props
        {
        }

        public class State
        {
            public string[][] history;
            public int stepNumber;
            public bool xIsNext;
        }
    }
}
