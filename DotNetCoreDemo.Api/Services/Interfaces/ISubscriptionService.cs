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

        public ServiceResult<List<Subscription>> GetSubscriptionsByUser(string userName);

        public ServiceResult<bool> Delete(int id);
    }
}
