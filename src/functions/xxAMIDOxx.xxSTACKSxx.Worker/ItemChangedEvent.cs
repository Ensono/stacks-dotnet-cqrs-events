using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;

namespace xxAMIDOxx.xxSTACKSxx.Worker
{
    public class ItemChangedEvent : IApplicationEvent
    {
        public ItemChangedEvent(int operationCode, Guid correlationId, string id, string eTag)
        {
            OperationCode = operationCode;
            CorrelationId = correlationId;
            Id = id;
            ETag = eTag;
        }

        public int EventCode => 999;
        public int OperationCode { get; private set; }
        public Guid CorrelationId { get; private set; }
        public string Id { get; private set; }
        public string ETag { get; private set; }
    }
}
