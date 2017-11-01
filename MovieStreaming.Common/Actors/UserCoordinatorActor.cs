using System;
using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Common.MessageTypes;

namespace MovieStreaming.Common.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;
        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();
            Receive<PlayMovieMessage>(
                message =>
                {
                    CreateChildUserIfNotExist(message.UserId);
                    IActorRef childActor = _users[message.UserId];
                    childActor.Tell(message);
                });

            Receive<StopMovieMessage>(
                message =>
                {
                    CreateChildUserIfNotExist(message.UserId);
                    IActorRef childActor = _users[message.UserId];
                    childActor.Tell(message);
                });
        }

        private void CreateChildUserIfNotExist(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                IActorRef newChildActorRef =
                    Context.ActorOf(Props.Create(() => new UserActor(userId)), "User" + userId);
                _users.Add(userId, newChildActorRef);
                Colorconsole.WriteLineCyan($"UserCoordinatorActor created new child UserActor for {userId} (Total User: {_users.Count})");
            }
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            Colorconsole.WriteLineCyan("UserCoordinatorActor PreStart");
        }

        protected override void PostStop()
        {
            Colorconsole.WriteLineCyan("UserCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Colorconsole.WriteLineCyan($"UserCoordinatorActor PreRestart because: {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Colorconsole.WriteLineCyan($"UserCoordinatorActor PostRestart because: {reason}");
            base.PostRestart(reason);
        }
        #endregion
    }
}
