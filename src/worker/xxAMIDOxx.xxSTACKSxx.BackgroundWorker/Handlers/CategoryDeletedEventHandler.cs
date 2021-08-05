using System.Threading.Tasks;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Events;
using Microsoft.Extensions.Logging;

namespace xxAMIDOxx.xxSTACKSxx.BackgroundWorker.Handlers
{
	public class CategoryDeletedEventHandler : IApplicationEventHandler<CategoryDeletedEvent>
	{
		private readonly ILogger<CategoryDeletedEventHandler> log;

		public CategoryDeletedEventHandler(ILogger<CategoryDeletedEventHandler> log)
		{
			this.log = log;
		}

		public Task HandleAsync(CategoryDeletedEvent appEvent)
		{
			log.LogInformation($"Executing CategoryDeletedEventHandler...");
			return Task.CompletedTask;
		}
	}
}
