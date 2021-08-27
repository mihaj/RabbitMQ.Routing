using System;
using System.Threading.Tasks;
using MassTransit;
using Newtonsoft.Json;

namespace Core
{
    public class MyConsumer: IConsumer<MyMessage>
    {
        public Task Consume(ConsumeContext<MyMessage> context)
        {
            Console.WriteLine(JsonConvert.SerializeObject(context.Message));

            return Task.CompletedTask;
        }
    }
}