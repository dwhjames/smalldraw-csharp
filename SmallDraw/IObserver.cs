
namespace SmallDraw
{
    /// <summary>
    /// This represents an object type that observes a figure.
    /// The update method is called whenever the figure moves.
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Called by the observable that this observer is interested in,
        /// this is the event indicating that the observable has changed.
        /// </summary>
        void Update();
    }
}
