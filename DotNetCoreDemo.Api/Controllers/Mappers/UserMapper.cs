using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Controllers.Mappers
{
    public static class UserMapper
    {
        public static Data.User MapToService(User controllerUser)
        {
            return new Data.User()
            {
                EmailAddress = controllerUser.EmailAddress,
                FirstName = controllerUser.FirstName,
                LastName = controllerUser.LastName,
                Username = controllerUser.Username
            };
        }

        public static User MapToController(Data.User serviceUser)
        {
            return new User()
            {
                EmailAddress = serviceUser.EmailAddress,
                FirstName = serviceUser.FirstName,
                LastName = serviceUser.LastName,
                Username = serviceUser.Username
            };
        }
    }
}
