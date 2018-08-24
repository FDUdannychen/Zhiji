using System;
using System.Collections.Generic;
using System.Text;

namespace Zhiji.IntegrationEventLog
{
    public enum IntegrationEventStatus
    {
        NotPublished = 0,
        Published = 1,
        PublishFailed = 2
    }
}
