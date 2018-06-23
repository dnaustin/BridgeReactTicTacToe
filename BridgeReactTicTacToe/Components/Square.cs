using Bridge.React;
using System;

namespace BridgeReactTicTacToe.Components
{
    public class Square : StatelessComponent<Square.Props>
    {
        public Square(Props props) : base(props) { }

        public override ReactElement Render()
        {
            return DOM.Button(new ButtonAttributes
            {
                ClassName = "square",
                OnClick = e => this.props.onClick()
            }, this.props.value);
        }

        public class Props
        {
            public string value;
            public Action onClick;
        }
    }
}
