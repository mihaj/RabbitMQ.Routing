using System.Diagnostics.CodeAnalysis;
using Core;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace RabbitMQ_Consumer
{
    [ExcludeFromCodeCoverage]
    public static class AddMassTransitServiceExtensions
    {
        public static IServiceCollection AddMassTransitService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<MyConsumer>();
                cfg.UsingRabbitMq((context, configurator) =>
                {

                    configurator.Host("localhost",
                        5672,
                        "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                    var queueName = $"{KebabCaseEndpointNameFormatter.Instance.Consumer<MyConsumer>()}-{configuration["TenantId"]}";
                    
                    configurator.ReceiveEndpoint(queueName, endpoint =>
                    {
                        endpoint.ConfigureConsumeTopology = false;
                        endpoint.Bind<MyMessage>(x =>
                        {
                            x.ExchangeType = ExchangeType.Direct;
                            x.RoutingKey = configuration["TenantId"];
                        });

                        endpoint.ConfigureConsumer<MyConsumer>(context);
                    });

                });
            });

            services.AddHostedService<MassTransitHostedService>();

            return services;
        }
    }
}