
using System.Collections.Generic;
using System.Drawing;

namespace SmallDraw.Basic
{
    /// <summary>
    /// A basic implementation of the IFigure interface,
    /// it provides common elements to produce bounding rectangles,
    /// a list of observers, a list of handles, and others.
    /// </summary>
    public abstract class BasicFigure : IFigure, IObservable
    {
        #region fields
        /// <summary>
        /// the selection status of the figure
        /// </summary>
        protected bool _selected = false;

        /// <summary>
        /// the figure's dimensions
        /// </summary>
        protected Point _location;
        protected Size _size;

        /// <summary>
        /// a set of observers subscribed to the events of a figure
        /// </summary>
        private ISet<IObserver> _observers = new HashSet<IObserver>();

        /// <summary>
        /// the set of handles used to manipulate the figure
        /// </summary>
        protected IList<IHandle> _handles;

        /// <summary>
        /// the canvas on which this figure is drawn
        /// </summary>
        protected ICanvas _canvas;
        #endregion

        #region constructors
        /// <summary>
        /// Initialize a basic figure along with its handles
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="location">the location of the figure</param>
        /// <param name="size">the size of the figure</param>
        public BasicFigure(ICanvas canvas, Point location, Size size)
        {
            this._canvas = canvas;
            this._location = location;
            this._size = size;
            this.InitializeHandles();
        }

        /// <summary>
        /// Initialize a basic figure along with its handles
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        public BasicFigure(ICanvas canvas, int x, int y, int width, int height)
            : this(canvas, new Point(x, y), new Size(width, height))
        { }

        /// <summary>
        /// Initialize a basic figure along with its handles
        /// </summary>
        /// <param name="canvas">the associated canvas</param>
        /// <param name="bounds">the bounds</param>
        public BasicFigure(ICanvas canvas, Rectangle bounds)
            : this(canvas, bounds.Location, bounds.Size)
        { }

        /// <summary>
        /// Initialize the handles list, this is called by the constructor and 
        /// is expected to be overridden in subclasses that require different handles.
        /// The default handles provided are a LocatorHandle centered on the figure
        /// and a ConnectorHandle near the top left corner.
        /// </summary>
        protected virtual void InitializeHandles()
        {
            _handles = new List<IHandle>();
            _handles.Add(new LocatorHandle(new Locator.ProportionalLocator(this), _canvas));
            _handles.Add(new Figure.Line.ConnectorHandle(new Locator.ProportionalLocator(this), _canvas, this));
        }
        #endregion

        /// <summary>
        /// To be called by the canvas to cause the figure to draw itself
        /// </summary>
        /// <param name="g">the graphics context</param>
        public abstract void Paint(Graphics g);

        #region implementation of IFigure
        /// <summary>
        /// Get the bounds of the figure
        /// </summary>
        public virtual Rectangle Bounds
        {
            get
            {
                return new Rectangle(_location, _size);
            }
        }

        /// <summary>
        /// Get the bounds that enclose the figure and any handles
        /// </summary>
        public Rectangle ExpandedBounds
        {
            get
            {
                var rect = this.Bounds;
                rect.Inflate((LocatorHandle.WIDTH / 2) + 1, (LocatorHandle.HEIGHT / 2) + 1);
                return rect;
            }
        }

        /// <summary>
        /// Get an enumeration of the handles for the figure
        /// </summary>
        public virtual IEnumerable<IHandle> Handles
        {
            get { return _handles; }
        }

        /// <summary>
        /// Get a locator for the center of the figure
        /// </summary>
        public virtual ILocator CenterLocator
        {
            get { return _handles[0].Locator; }
        }

        /// <summary>
        /// Get and set the selection status of the figure
        /// </summary>
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                _canvas.Repaint(this.ExpandedBounds);
            }
        }

        /// <summary>
        /// Test if a given point touches the figure
        /// </summary>
        /// <param name="p">the point to test</param>
        /// <returns>true if the point touches the figure</returns>
        public virtual bool Touches(Point p)
        {
            return false;
        }

        /// <summary>
        /// Get and set the location of the figure
        /// </summary>
        public virtual Point Location
        {
            get
            {
                return _location;
            }
            set
            {
                var oldBounds = this.ExpandedBounds;
                _location = value;
                NotifyObservers();
                var newBounds = this.ExpandedBounds;
                _canvas.Repaint(Rectangle.Union(oldBounds, newBounds));
            }
        }

        /// <summary>
        /// Translate the figure by a width and a height
        /// </summary>
        /// <param name="s">the translation dimensions</param>
        public virtual void Translate(Size s)
        {
            this.Location = Point.Add(this.Location, s);
        }

        /// <summary>
        /// Get and set the size of the figure
        /// </summary>
        public virtual Size Size
        {
            get
            {
                return _size;
            }
            set
            {
                var oldBounds = this.ExpandedBounds;
                _size = value;
                NotifyObservers();
                var newBounds = this.ExpandedBounds;
                _canvas.Repaint(Rectangle.Union(oldBounds, newBounds));
            }
        }

        /// <summary>
        /// Get a locator for this figure, relative to a given point
        /// </summary>
        /// <param name="p">a point</param>
        /// <returns>a locator</returns>
        public ILocator RelativeLocator(Point p)
        {
            return new Locator.ProportionalLocator(this,
                                                   ((float)(p.X - this.Location.X)) / ((float)this.Size.Width),
                                                   ((float)(p.Y - this.Location.Y)) / ((float)this.Size.Height));
        }

        /*
        public abstract IFigure clone(int x, int y);
        */
        #endregion

        #region implementation of IObservable
        /// <summary>
        /// Add an observer to the set of observers on this figure
        /// </summary>
        /// <param name="obs">the observer to add</param>
        void IObservable.AddObserver(IObserver obs)
        {
            _observers.Add(obs);
        }

        /// <summary>
        /// notify all the observers that the state of the figure has changed
        /// </summary>
        protected void NotifyObservers()
        {
            foreach (var obs in _observers)
            {
                obs.Update();
            }
        }
        #endregion

        /// <summary>
        /// A helper method to keep the dimensions of a figure in sync.
        /// If a subclass overrides the bounds property in such a way
        /// that it no longer depends on _location and _size, it should
        /// called this method to update _location and _size
        /// </summary>
        protected void RecomputeShapeFromBounds()
        {
            var newBounds = this.Bounds;
            _location = newBounds.Location;
            _size = newBounds.Size;
        }
    }   
}
