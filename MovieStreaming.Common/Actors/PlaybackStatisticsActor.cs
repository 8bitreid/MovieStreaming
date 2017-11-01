using System;
using Akka.Actor;
using MovieStreaming.Common.Exceptions;

namespace MovieStreaming.Common.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(  
                ex =>
                {
                    if (ex is SimulatedCorruptStateException)
                    {
                        return Directive.Restart;
                    }
                    if (ex is SimulatedTerribleMovieException)
                    {
                        return Directive.Resume;
                    }

                    return Directive.Restart;
                }
                );
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            Colorconsole.WriteLineWhite("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            Colorconsole.WriteLineWhite("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Colorconsole.WriteLineWhite($"PlaybackStatisticsActor PreRestart because: {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Colorconsole.WriteLineWhite($"PlaybackStatisticsActor PostRestart because: {reason}");
            base.PostRestart(reason);
        }
        #endregion
    }
}
