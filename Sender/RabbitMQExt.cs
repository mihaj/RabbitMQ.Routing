using System.Diagnostics.CodeAnalysis;
using Core;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMQ_Sender
{
    [ExcludeFromCodeCoverage]
    public static class AddMassTransitServiceExtensions
    {
        public static IServiceCollection AddMassTransitService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host("localhost",
                        5672,
                        "/", h =>
                        {
                           h.Username("guest");
                           h.Password("guest");
                        });
                });
            });

            services.AddHostedService<MassTransitHostedService>();

            return services;
        }
    }
}