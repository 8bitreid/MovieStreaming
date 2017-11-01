using System;
using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Common.Exceptions;
using MovieStreaming.Common.MessageTypes;

namespace MovieStreaming.Common.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;
        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();
            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts[message.MovieTitle] += 1;
            }
            else
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }

            // Simulated Bugs
            if (_moviePlayCounts[message.MovieTitle] > 3)
            {
                throw new SimulatedCorruptStateException();
            }
            if (message.MovieTitle == "Partial Recoil")
            {
                throw new SimulatedTerribleMovieException();
            }
            Colorconsole.WriteLineMagenta($"MoviePlayCounterActor {message.MovieTitle} has been watched {_moviePlayCounts[message.MovieTitle]} times");
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            Colorconsole.WriteLineMagenta("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            Colorconsole.WriteLineMagenta("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Colorconsole.WriteLineMagenta($"PlaybackStatisticsActor PreRestart because: {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Colorconsole.WriteLineMagenta($"PlaybackStatisticsActor PostRestart because: {reason}");
            base.PostRestart(reason);
        }
        #endregion
    }
}
