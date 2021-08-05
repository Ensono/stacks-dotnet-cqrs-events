using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace xxAMIDOxx.xxSTACKSxx.BackgroundWorker.Handlers
{
	public class CategoryUpdatedEventHandler : IApplicationEventHandler<CategoryUpdatedEvent>
	{
		private readonly ILogger<CategoryUpdatedEventHandler> log;

		public CategoryUpdatedEventHandler(ILogger<CategoryUpdatedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(CategoryUpdatedEvent appEvent)
		{
			log.LogInformation($"Executing CategoryUpdatedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
