using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using RabbitMQ.Client;

namespace Core
{
    public static class Topology
    {
        public static void ConfigureMessageTopology(this IRabbitMqBusFactoryConfigurator configurator)
        {
            configurator.Send<MyMessage>(x =>
            {
                x.UseRoutingKeyFormatter<MyMessage>(context => context.Message.TenantId);
            });
            
            configurator.Message<MyMessage>(x => x.SetEntityName(KebabCaseEndpointNameFormatter.Instance.Message<MyMessage>()));

            configurator.Publish<MyMessage>(x =>
            {
                x.ExchangeType = ExchangeType.Direct;
            });
        }
    }
}