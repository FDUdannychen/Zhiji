using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Zhiji.Common.Json;
using Zhiji.EventBus;

namespace Zhiji.IntegrationEventLog
{
    public class IntegrationEventEntry
    {
        private static readonly JsonSerializerSettings _jsonSettings
            = new JsonSerializerSettings { ContractResolver = new DeclaredPropertiesResolver() }
            .ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);

        public Guid Id { get; private set; }
        public Instant CreateTime { get; private set; }
        public string Type { get; private set; }
        public string Arguments { get; private set; }
        public IntegrationEventStatus Status { get; set; }
        public int PublishTimes { get; set; }

        private IntegrationEventEntry() { }

        public IntegrationEventEntry(IntegrationEvent evt)
        {
            this.Id = evt.Id;
            this.CreateTime = evt.CreateTime;
            this.Type = evt.GetType().Name;
            this.Arguments = JsonConvert.SerializeObject(evt, _jsonSettings);
            this.Status = IntegrationEventStatus.NotPublished;
            this.PublishTimes = 0;
        }
    }
}
