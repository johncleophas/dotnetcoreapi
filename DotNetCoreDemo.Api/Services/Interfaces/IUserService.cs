using DotNetCoreDemo.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Services
{
    public interface IUserService
    {
        public ServiceResult<User> Authenticate(string username, string password);

        public ServiceResult<User> Create(User user, string password);

        public ServiceResult<User> GetUserByUserName(string userName);

        public ServiceResult<bool> Delete(int id);

    }
}
