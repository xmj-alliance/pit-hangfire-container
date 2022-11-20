using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using HangfireContainer.Job;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HangfireContainer.Dashboard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WelcomeController : ControllerBase
    {

        private readonly ILogger<WelcomeController> _logger;
        private readonly IBackgroundJobClient _client;

        public WelcomeController(
            ILogger<WelcomeController> logger,
            IBackgroundJobClient client
        )
        {
            _logger = logger;
            _client = client;
        }

        [HttpPost]
        [Route("welcome")]
        public IActionResult WelcomeUser([FromBody] InputWelcome inputWelcome)
        {
            var jobID = _client.Enqueue<Welcome>((c) => c.SendWelcome(inputWelcome.UserName));
            return Ok(new {
                OK = true,
                Message = $"Job Id {jobID} Completed. Welcome Sent!"
            });
        }

    }

    public record InputWelcome(string UserName);

}
