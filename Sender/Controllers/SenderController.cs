using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RabbitMQ_Sender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SenderController : ControllerBase
    {
        private readonly ILogger<SenderController> _logger;
        private readonly Sender _sender;

        public SenderController(ILogger<SenderController> logger, Sender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpGet]
        public async Task Send(string tenantId)
        {
           await _sender.Publish(new MyMessage()
            {
                ID = "23423",
                Name = "My message",
                TenantId = tenantId
            });

           Ok();
        }
    }
}