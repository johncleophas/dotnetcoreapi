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
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> logger;
        private IBooksService booksService;
        private readonly AppSettings appSettings;
        public BookController(ILogger<BookController> logger, IBooksService booksService, AppSettings appSettings)
        {
            this.logger = logger;
            this.booksService = booksService;
            this.appSettings = appSettings;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var booksResults = booksService.GetBooks();

            return StatusCode((int)booksResults.Status, BookMapper.MapBooksToController(booksResults.Result));
        }
    }
}
