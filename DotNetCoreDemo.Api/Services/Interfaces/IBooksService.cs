using DotNetCoreDemo.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Services
{
    public interface IBooksService
    {
        public void GenerateSampleBooks();

        public ServiceResult<List<Book>> GetBooks();
    }
}
