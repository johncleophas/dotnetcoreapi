using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Services
{
    public class ServiceResult<T>
    {
        public T Result { get; set; }

        public string Message { get; set; }

        public HttpStatusCode Status { get; set; }
    }
}
