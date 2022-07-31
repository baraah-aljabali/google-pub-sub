using Google.PubSub.Publisher;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google.PubSub.NetCore.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController: ControllerBase
    {
        private readonly IPublisherService _publisherService;
        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(PublisherRequest publisherRequest)
        {
            return Ok(await _authenticationService.RegisterAsync(request));
        }
    }
}
