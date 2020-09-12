using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Utilities.EventBus
{
    public interface IBus
    {
        void Publish(IntegrationEvent @event);
    }
}
