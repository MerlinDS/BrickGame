namespace BrickGame.Scripts
{
    /// <summary>
    /// Playground external notifications
    /// </summary>
    public static class PlaygroundNotification
    {

        /// <summary>
        /// Start new game
        /// </summary>
        public const string Start = "PlaygroundNotification::Start";
        /// <summary>
        /// The game that was previously started is ended.
        /// </summary>
        public const string End = "PlaygroundNotification::End";

        /// <summary>
        /// Set playground on pause or resume it.
        /// </summary>
        public const string Pause = "PlaygroundNotification:Pause";
    }
}