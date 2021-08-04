using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace xxAMIDOxx.xxSTACKSxx.BackgroundWorker.Handlers
{
    public class MenuCreatedEventHandler : IApplicationEventHandler<MenuCreatedEvent>
    {
        private readonly ILogger<MenuCreatedEventHandler> log;

        public MenuCreatedEventHandler(ILogger<MenuCreatedEventHandler> log)
        {
            this.log = log;
        }

        public Task HandleAsync(MenuCreatedEvent appEvent)
        {
            log.LogInformation($"Executing MenuCreatedEventHandler...");
            return Task.CompletedTask;
        }
    }
}