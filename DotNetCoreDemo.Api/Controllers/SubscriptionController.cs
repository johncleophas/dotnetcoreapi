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
        private IUserService userService;
        private readonly AppSettings appSettings;
        public SubscriptionController(ILogger<UserController> logger, ISubscriptionService subscriptionService, AppSettings appSettings, IUserService userService)
        {
            this.logger = logger;
            this.subscriptionService = subscriptionService;
            this.appSettings = appSettings;
            this.userService = userService;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] Subscription model)
        {

            var user = this.userService.GetUserByUserName(model.UserName);

            var serviceSubscription = SubscriptionMapper.MapToService(model, user.Result.Id);

            var createdSubscription = subscriptionService.Create(serviceSubscription);

            return StatusCode((int)createdSubscription.Status, SubscriptionMapper.MapToController(createdSubscription.Result));
        }

        [HttpGet("availablebooks/{userName}")]
        public IActionResult GetAvailableBooksByUser(string userName) {
            var subscriptionResult = subscriptionService.GetAvailableBooksByUser(userName);

            return StatusCode((int)subscriptionResult.Status, subscriptionResult.Result);
        }

        [HttpGet("subscribedbooks/{userName}")]
        public IActionResult GetSubscribedBooksByUser(string userName) {
            var subscriptionResult = subscriptionService.GetSubscribedBooksByUser(userName);

            return StatusCode((int)subscriptionResult.Status, subscriptionResult.Result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var subscriptionResult = subscriptionService.Delete(id);

            return StatusCode((int)subscriptionResult.Status, true);
        }
    }
}
