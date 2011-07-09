using System.Drawing;

namespace SmallDraw.Util
{
    public class PointRef
    {
        protected Point _point;

        public PointRef()
        {
            _point = new Point(0, 0);
        }

        public PointRef(PointRef p)
            : this(p.X, p.Y)
        { }

        public PointRef(Point p)
        {
            _point = p;
        }

        public PointRef(int x, int y)
        {
            _point = new Point(x, y);
        }

        public int X
        {
            get { return _point.X; }
            set { _point.X = value; }
        }

        public int Y
        {
            get { return _point.Y; }
            set { _point.Y = value; }
        }

        public void Set(PointRef p)
        {
            this.Set(p.X, p.Y);
        }

        public void Set(Point p)
        {
            this.Set(p.X, p.Y);
        }

        public void Set(int x, int y)
        {
            _point.X = x;
            _point.Y = y;
        }

        public void Add(PointRef p)
        {
            _point.Offset(p.X, p.Y);
        }

        public void Add(Point p)
        {
            _point.Offset(p);
        }

        public void Add(int x, int y)
        {
            _point.Offset(x, y);
        }

        public override string ToString()
        {
            return _point.ToString();
        }
    }
}
