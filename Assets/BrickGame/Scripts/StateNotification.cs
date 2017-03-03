using BrickGame.Scripts.Controllers.Commands;

namespace BrickGame.Scripts
{
    /// <summary>
    /// A game session states notification.
    /// These notifications will cahnge state of the game session
    /// </summary>
    public static class StateNotification
    {

        /// <summary>
        /// Start new game session if it was not started yet.
        /// Execute <see cref="StartGameCommand"/>
        /// </summary>
        public const string Start = "StateNotification::Start";
        /// <summary>
        /// The game session that was previously started is finished.
        /// Save user progress and clean game playground.
        /// </summary>
        public const string End = "StateNotification::End";
        /// <summary>
        /// Set game on pause or resume it.
        /// </summary>
        public const string Pause = "StateNotification:Pause";
        /// <summary>
        /// Try to restore playground from the cache.
        /// Execute <see cref="RestoreGameCommand"/>
        /// </summary>
        public static string Restore = "StateNotification:Restore";
    }
}