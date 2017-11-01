namespace MovieStreaming.Common.MessageTypes
{
    public class StopMovieMessage
    {
        public int UserId { get; private set; }
        public StopMovieMessage(int userId)
        {
            UserId = userId;
        }
    }
}
