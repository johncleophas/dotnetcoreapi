using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Controllers
{
    public class Subscription
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int BookId { get; set; }
    }
}
