
namespace SmallDraw
{
    /// <summary>
    /// An Observable object keeps a list of observers
    /// who are interested in update events, this interface
    /// provides the method for adding these observers to the list.
    /// </summary>
    public interface IObservable
    {
        /// <summary>
        /// Adds an observer to the list of observers interested in events
        /// </summary>
        /// <param name="obs">the observer to add</param>
        void AddObserver(IObserver obs);
    }
}
