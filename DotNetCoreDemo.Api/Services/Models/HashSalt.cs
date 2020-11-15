using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Services
{
    public class HashSalt
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
