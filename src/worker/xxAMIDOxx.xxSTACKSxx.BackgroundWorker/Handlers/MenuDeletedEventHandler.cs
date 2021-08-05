using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace xxAMIDOxx.xxSTACKSxx.BackgroundWorker.Handlers
{
	public class MenuDeletedEventHandler : IApplicationEventHandler<MenuDeletedEvent>
	{
		private readonly ILogger<MenuDeletedEventHandler> log;

		public MenuDeletedEventHandler(ILogger<MenuDeletedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(MenuDeletedEvent appEvent)
		{
			log.LogInformation($"Executing MenuDeletedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
