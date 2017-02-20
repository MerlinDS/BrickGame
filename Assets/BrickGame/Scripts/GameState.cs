namespace BrickGame.Scripts
{
    /// <summary>
    /// Game state notification
    /// </summary>
    public static class GameState
    {

        /// <summary>
        /// Rebuild new game
        /// </summary>
        public const string Start = "GameState::Start";
        /// <summary>
        /// The game that was previously started is ended.
        /// </summary>
        public const string End = "GameState::End";

        /// <summary>
        /// Set playground on pause or resume it.
        /// </summary>
        public const string Pause = "GameState:Pause";

        public static string Restore = "GameState:Restore";
    }
}