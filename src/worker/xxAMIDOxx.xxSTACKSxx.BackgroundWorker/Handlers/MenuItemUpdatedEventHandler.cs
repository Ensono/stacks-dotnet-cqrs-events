using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace xxAMIDOxx.xxSTACKSxx.BackgroundWorker.Handlers
{
	public class MenuItemUpdatedEventHandler : IApplicationEventHandler<MenuItemUpdatedEvent>
	{
		private readonly ILogger<MenuItemUpdatedEventHandler> log;

		public MenuItemUpdatedEventHandler(ILogger<MenuItemUpdatedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(MenuItemUpdatedEvent appEvent)
		{
			log.LogInformation($"Executing MenuItemUpdatedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
