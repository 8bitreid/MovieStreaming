namespace MovieStreaming.Common.MessageTypes
{
    public class PlayMovieMessage
    {
        public int UserId { get; private set; }
        public string MovieTitle { get; private set; }

        public PlayMovieMessage(string movieTitle, int userId)
        {
            MovieTitle = movieTitle;
            UserId = userId;
        }
    }
}
