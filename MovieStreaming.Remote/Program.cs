using Akka.Actor;
using MovieStreaming.Common;

namespace MovieStreaming.Remote
{
    class Program
    {
        private static ActorSystem _movieStreamingActorSystem;
        static void Main(string[] args)
        {
            Colorconsole.WriteLineGray("Creating MovieStreamingActorSystem");
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            _movieStreamingActorSystem.AwaitTermination();

        }
    }
}
