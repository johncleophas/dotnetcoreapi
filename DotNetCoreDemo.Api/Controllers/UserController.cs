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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private IUserService userService;
        private readonly AppSettings appSettings;
        public UserController(ILogger<UserController> logger, IUserService userService, AppSettings appSettings)
        {
            this.logger = logger;
            this.userService = userService;
            this.appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] User model)
        {
            var serviceUser = UserMapper.MapToService(model);

            var createdUser = userService.Create(serviceUser, model.Password);

            if (createdUser.Result == null) {
                return StatusCode((int)createdUser.Status, createdUser.Message);
            }

            return StatusCode((int)createdUser.Status, UserMapper.MapToController(createdUser.Result));
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            var serviceResult = new ServiceResult<User>()
            {
                Message = "Not authorised",
                Result = null,
                Status = System.Net.HttpStatusCode.Unauthorized
            };


            var result = userService.Authenticate(user.UserName, user.Password);

            var userDetails = userService.GetUserByUserName(user.UserName);

            if (result.Result == null)
                return StatusCode((int)serviceResult.Status, result.Message);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                UserName = userDetails.Result.UserName,
                FirstName = userDetails.Result.FirstName,
                LastName = userDetails.Result.LastName,
                Token = tokenString
            });
        }

        [HttpGet("{userName}")]
        public IActionResult GetByUserName(string userName)
        {
            var user = userService.GetUserByUserName(userName);
            return StatusCode((int)user.Status, UserMapper.MapToController(user.Result));
        }
    }
}
