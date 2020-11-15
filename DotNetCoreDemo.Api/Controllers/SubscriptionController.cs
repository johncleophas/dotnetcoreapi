using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreDemo.Api.Controllers.Mappers;
using DotNetCoreDemo.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DotNetCoreDemo.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private ISubscriptionService subscriptionService;
        private readonly AppSettings appSettings;
        public SubscriptionController(ILogger<UserController> logger, ISubscriptionService subscriptionService, AppSettings appSettings)
        {
            this.logger = logger;
            this.subscriptionService = subscriptionService;
            this.appSettings = appSettings;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] Subscription model)
        {
            var serviceSubscription = SubscriptionMapper.MapToService(model);

            var createdSubscription = subscriptionService.Create(serviceSubscription);

            return StatusCode((int)createdSubscription.Status, SubscriptionMapper.MapToController(createdSubscription.Result));
        }

        [HttpGet("{username}")]
        public IActionResult GetSubscriptionsByUser(string userName)
        {
            var subscriptionResult = subscriptionService.GetSubscriptionsByUser(userName);

            return StatusCode((int)subscriptionResult.Status, SubscriptionMapper.MapSubscriptionsToController(subscriptionResult.Result));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var subscriptionResult = subscriptionService.Delete(id);

            return StatusCode((int)subscriptionResult.Status, true);
        }
    }
}
