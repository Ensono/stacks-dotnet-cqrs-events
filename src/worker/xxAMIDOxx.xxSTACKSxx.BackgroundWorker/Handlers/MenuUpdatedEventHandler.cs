using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace xxAMIDOxx.xxSTACKSxx.BackgroundWorker.Handlers
{
	public class MenuUpdatedEventHandler : IApplicationEventHandler<MenuUpdatedEvent>
	{
		private readonly ILogger<MenuUpdatedEventHandler> log;

		public MenuUpdatedEventHandler(ILogger<MenuUpdatedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(MenuUpdatedEvent appEvent)
		{
			log.LogInformation($"Executing MenuUpdatedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
