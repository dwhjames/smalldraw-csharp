
using System.Drawing;
using System.Collections.Generic;

namespace SmallDraw.Locator
{
    /// <summary>
    /// This locates a position based on a point object that is not copied at consturction
    /// </summary>
    public class PointLocator : ILocator, IObservable
    {
        /// <summary>
        /// The point from which the location is derived
        /// </summary>
        protected Point _point;

        /// <summary>
        /// The observers
        /// </summary>
        protected ISet<IObserver> _observers = new HashSet<IObserver>();

        /// <summary>
        /// Instantiates a new point locator
        /// </summary>
        public PointLocator()
        {
            this._point = new Point(0, 0);
        }

        /// <summary>
        /// Construct the locator with the given point for later reference
        /// </summary>
        /// <param name="point">the point the locator is associated with</param>
        public PointLocator(Point point)
        {
            this._point = point;
        }

        /// <summary>
        /// Get the x coordinate
        /// </summary>
        public int X
        {
            get { return _point.X; }
        }

        /// <summary>
        /// Get the y coordinate
        /// </summary>
        public int Y
        {
            get { return _point.Y; }
        }

        /// <summary>
        /// Get and set the x,y location
        /// </summary>
        public Point Location
        {
            get { return _point; }
            set
            {
                _point = value;
                NotifyObservers();
            }
        }

        /// <summary>
        /// Translate the locator by translating the underying point
        /// </summary>
        /// <param name="s">the translation</param>
        public void Translate(Size s)
        {
            _point.Offset(s.Width, s.Height);
            NotifyObservers();
        }

        /// <summary>
        /// Add an observer to the set of observers for this object
        /// </summary>
        /// <param name="obs">the observer to be added</param>
        void IObservable.AddObserver(IObserver obs)
        {
            _observers.Add(obs);
        }

        /// <summary>
        /// Message each observer to say that the state of this object has changed
        /// </summary>
        protected void NotifyObservers()
        {
            foreach (var obs in _observers)
            {
                obs.Update();
            }
        }
    }
}
