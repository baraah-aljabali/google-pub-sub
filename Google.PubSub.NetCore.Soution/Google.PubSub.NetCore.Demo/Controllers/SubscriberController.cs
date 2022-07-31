using Google.PubSub.Subscriber;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google.PubSub.NetCore.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;
        public SubscriberController(ISubscriberService subscriberService)
        {
            this._subscriberService = subscriberService;
        }

        [HttpPost("messagecount")]
        public async Task<ActionResult<int>> GetMessageCount(SubscriberRequest subscriberRequest)
        {
            return Ok(await _subscriberService.PullMessagesAsync(subscriberRequest));
        }
    }
}
