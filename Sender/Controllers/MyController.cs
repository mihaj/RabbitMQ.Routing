using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RabbitMQ_Sender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<MyController> _logger;
        private readonly Sender _sender;

        public MyController(ILogger<MyController> logger, Sender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpGet]
        public async Task Get(string tenantId)
        {
           await _sender.Publish(new MyMessage()
            {
                ID = "23423",
                Name = "Miha Jakovac",
                TenantId = tenantId
            });

           Ok();
        }
    }
}