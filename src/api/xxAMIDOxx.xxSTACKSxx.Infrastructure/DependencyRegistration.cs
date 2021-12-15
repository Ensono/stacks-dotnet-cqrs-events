using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Application.CQRS.Commands;
using Amido.Stacks.Application.CQRS.Queries;
using Amido.Stacks.Configuration.Extensions;
using Amido.Stacks.DependencyInjection;
using Amido.Stacks.Messaging.Azure.ServiceBus.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using xxAMIDOxx.xxSTACKSxx.Application.CommandHandlers;
using xxAMIDOxx.xxSTACKSxx.Application.Integration;
using xxAMIDOxx.xxSTACKSxx.Application.QueryHandlers;
using xxAMIDOxx.xxSTACKSxx.Domain;
using xxAMIDOxx.xxSTACKSxx.Infrastructure.Fakes;
using xxAMIDOxx.xxSTACKSxx.Infrastructure.HealthChecks;

namespace xxAMIDOxx.xxSTACKSxx.Infrastructure
{
    public static class DependencyRegistration
    {
        static readonly ILogger log = Log.Logger;

        /// <summary>
        /// Register static services that does not change between environment or contexts(i.e: tests)
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureStaticDependencies(IServiceCollection services)
        {
            AddCommandHandlers(services);
            AddQueryHandlers(services);
        }

        /// <summary>
        /// Register dynamic services that changes between environments or context(i.e: tests)
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureProductionDependencies(WebHostBuilderContext context, IServiceCollection services)
        {
            services.AddSecrets();

            services.Configure<Amido.Stacks.Messaging.Azure.ServiceBus.Configuration.ServiceBusConfiguration>(context.Configuration.GetSection("ServiceBusConfiguration"));
            services.AddServiceBus();
            services.AddTransient<IApplicationEventPublisher, Amido.Stacks.Messaging.Azure.ServiceBus.Publisher.EventPublisher>();

            services.AddTransient<IMenuRepository, InMemoryMenuRepository>();

            var healthChecks = services.AddHealthChecks();
        }

        private static void AddCommandHandlers(IServiceCollection services)
        {
            log.Information("Loading implementations of {interface}", typeof(ICommandHandler<,>).Name);
            var definitions = typeof(CreateMenuCommandHandler).Assembly.GetImplementationsOf(typeof(ICommandHandler<,>));
            foreach (var definition in definitions)
            {
                log.Information("Registering '{implementation}' as implementation of '{interface}'", definition.implementation.FullName, definition.interfaceVariation.FullName);
                services.AddTransient(definition.interfaceVariation, definition.implementation);
            }
        }

        private static void AddQueryHandlers(IServiceCollection services)
        {
            log.Information("Loading implementations of {interface}", typeof(IQueryHandler<,>).Name);
            var definitions = typeof(GetMenuByIdQueryHandler).Assembly.GetImplementationsOf(typeof(IQueryHandler<,>));
            foreach (var definition in definitions)
            {
                log.Information("Registering '{implementation}' as implementation of '{interface}'", definition.implementation.FullName, definition.interfaceVariation.FullName);
                services.AddTransient(definition.interfaceVariation, definition.implementation);
            }
        }
    }
}
