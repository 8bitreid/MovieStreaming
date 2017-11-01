using System;
using Akka.Actor;
using MovieStreaming.Common.MessageTypes;

namespace MovieStreaming.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private readonly int _userId;
        private string _currentlyWatching;
        public UserActor(int userId)
        {
            _userId = userId;
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(
                message => Colorconsole.WriteLineRed(
                    "Error: cannont start playing another movie before stopping existing one"));
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(
                message => Colorconsole.WriteLineRed("Error: cannot stop if nothing is playing"));

            Colorconsole.WriteLineYellow("UserActor has now become Stopped");

                
        }

        private void StopPlayingCurrentMovie()
        {
            Colorconsole.WriteLineYellow($"user has stoped watching {_currentlyWatching}");
            _currentlyWatching = null;
            Become(Stopped);
            Colorconsole.WriteLineYellow("UserActore has become Stopped");
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            Colorconsole.WriteLineYellow($"UserActor {_userId } is currently watching {_currentlyWatching}");
            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter").Tell(new IncrementPlayCountMessage(movieTitle));
            Become(Playing);
            Colorconsole.WriteLineCyan("UserActor has now become Playing");
        }

        protected override void PreStart()
        {
            Colorconsole.WriteLineYellow("UserActor PreStart");
        }

        protected override void PostStop()
        {
            Colorconsole.WriteLineYellow("UserActor PostStop");
        }
        protected override void PreRestart(Exception reason, object message)
        {
            Colorconsole.WriteLineYellow($"UserActor PreRestart because: {reason}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Colorconsole.WriteLineYellow($"UserActor PreReStart because: {reason}");
            base.PostRestart(reason);
        }

    }
}
