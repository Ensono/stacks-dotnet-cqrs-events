using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace xxAMIDOxx.xxSTACKSxx.BackgroundWorker.Handlers
{
	public class MenuItemCreatedEventHandler : IApplicationEventHandler<MenuItemCreatedEvent>
	{
		private readonly ILogger<MenuItemCreatedEventHandler> log;

		public MenuItemCreatedEventHandler(ILogger<MenuItemCreatedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(MenuItemCreatedEvent appEvent)
		{
			log.LogInformation($"Executing MenuItemCreatedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
