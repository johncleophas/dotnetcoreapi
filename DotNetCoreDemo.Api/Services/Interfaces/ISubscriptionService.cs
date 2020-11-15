using DotNetCoreDemo.Api.Controllers.Models;
using DotNetCoreDemo.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Services
{
    public interface ISubscriptionService
    {

        public ServiceResult<Subscription> Create(Subscription subscription);

        public ServiceResult<List<UserBookSubscription>> GetSubscribedBooksByUser(string userName);

        public ServiceResult<List<UserBookSubscription>> GetAvailableBooksByUser(string userName);
        public ServiceResult<bool> Delete(int id);
    }
}
