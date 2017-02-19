namespace BrickGame.Scripts.Playgrounds
{
    /// <summary>
    /// Playground external notifications
    /// </summary>
    public static class PlaygroundNotification
    {

        /// <summary>
        /// Rebuild new game
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

        public static string Restore = "PlaygroundNotification:Restore";
    }
}