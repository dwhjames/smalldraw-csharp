using System.Drawing;

namespace SmallDraw.Locator
{
    class RightMidLocator : ReshapingLocator
    {
        public RightMidLocator(IFigure figure)
            : base(figure)
        { }


        public override int X
        {
            get { return _figure.Bounds.Right; }
        }

        public override int Y
        {
            get { return _figure.Bounds.Top + (_figure.Size.Height / 2); }
        }

        public override Point Location
        {
            get
            {
                return new Point(this.X, this.Y);
            }
            set
            {
                var rect = _figure.Bounds;
                if (rect.Left + MINWIDTH < value.X)
                {
                    _figure.Size = new Size(value.X - rect.Left, rect.Height);
                }
            }
        }
    }
}
