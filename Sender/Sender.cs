using System;
using System.Threading.Tasks;
using Core;
using MassTransit;
using MassTransit.Definition;

namespace RabbitMQ_Sender
{
    public class Sender
    {
        private readonly ISendEndpointProvider _sendEndpoint;

        public Sender(ISendEndpointProvider sendEndpoint)
        {
            _sendEndpoint = sendEndpoint;
        }

        public async Task Publish(MyMessage resp)
        {
            string consumerName = $"{KebabCaseEndpointNameFormatter.Instance.Consumer<MyConsumer>()}-{resp.TenantId}";

            ISendEndpoint endpoint = await _sendEndpoint.GetSendEndpoint(
                new Uri($"queue:{consumerName}"));

            await endpoint.Send(resp);
        }
    }
}