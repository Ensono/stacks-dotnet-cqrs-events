using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;

namespace xxAMIDOxx.xxSTACKSxx.Worker
{
    public class ItemChangedEvent : IApplicationEvent
    {
        public ItemChangedEvent(int operationCode, Guid correlationId)
        {
            OperationCode = operationCode;
            CorrelationId = correlationId;
        }

        public int EventCode => 999;

        public int OperationCode { get; private set; }

        public Guid CorrelationId { get; private set; }
    }
}