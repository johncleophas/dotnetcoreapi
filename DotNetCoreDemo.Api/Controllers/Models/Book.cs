using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Controllers
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public double Price { get; set; }
    }
}
