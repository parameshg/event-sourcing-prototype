using System;
using System.Collections.Generic;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using NinjaBoard.Events;

namespace NinjaBoard.Database.Repositories
{
    public class EventRepository : IEventRepository
    {
        private IEventStoreConnection Connection { get; set; }

        public EventRepository()
        {
            var connectionString = "ConnectTo=tcp://user:password@localhost:1113; HeartBeatTimeout=500";
            Connection = EventStoreConnection.Create(connectionString);
            Connection.ConnectAsync().GetAwaiter().GetResult();
        }

        public List<Event> GetEvents(Guid gameId)
        {
            var result = new List<Event>();

            foreach (var i in Connection.ReadStreamEventsForwardAsync(gameId.ToString(), 0, 100, false).GetAwaiter().GetResult().Events)
            {
                if (i.OriginalEvent.EventType.Equals("GameStartedEvent"))
                    result.Add(JsonConvert.DeserializeObject<GameStartedEvent>(Encoding.ASCII.GetString(i.OriginalEvent.Data)));

                if (i.OriginalEvent.EventType.Equals("CoinMovedEvent"))
                    result.Add(JsonConvert.DeserializeObject<CoinMovedEvent>(Encoding.ASCII.GetString(i.OriginalEvent.Data)));
            }

            return result;
        }

        public void SendEvent(Guid gameId, Event @event)
        {
            Connection.AppendToStreamAsync(gameId.ToString(), -2, new EventData(Guid.NewGuid(), @event.GetType().Name, true, Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(@event)), null)).GetAwaiter().GetResult();
        }
    }
}