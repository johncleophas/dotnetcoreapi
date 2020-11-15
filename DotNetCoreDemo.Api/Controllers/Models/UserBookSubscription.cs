using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Controllers.Models
{
    public class UserBookSubscription
    {

        public int SubscriptionId { get; set; }

        public string Username { get; set; }

        public int BookId { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public double Price { get; set; }
    }
}
