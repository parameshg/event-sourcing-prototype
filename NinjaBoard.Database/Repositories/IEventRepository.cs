using System;
using System.Collections.Generic;
using NinjaBoard.Events;

namespace NinjaBoard.Database.Repositories
{
    public interface IEventRepository
    {
        List<Event> GetEvents(Guid gameId);

        void SendEvent(Guid gameId, Event @event);
    }
}