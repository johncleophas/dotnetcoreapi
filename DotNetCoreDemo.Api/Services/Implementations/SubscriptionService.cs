using DotNetCoreDemo.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Services
{
    public class SubscriptionService : ISubscriptionService
    {

        private DataContext context;

        public SubscriptionService(DataContext context)
        {
            this.context = context;
        }

        public ServiceResult<Subscription> Create(Subscription subscription)
        {
            this.context.Subscriptions.Add(subscription);
            this.context.SaveChanges();

            return new ServiceResult<Subscription>()
            {
                Result = subscription,
                Status = System.Net.HttpStatusCode.OK
            };
        }

        public ServiceResult<List<Subscription>> GetSubscriptionsByUser(string userName)
        {

            var user = this.context.Users.FirstOrDefault(e => e.Username == userName);

            if (user == null)
            {
                throw new ArgumentException($"Username {user.Username} not found");
            }

            return new ServiceResult<List<Subscription>>()
            {
                Result = this.context.Subscriptions.Where(e => e.UserId == user.Id).ToList(),
                Status = System.Net.HttpStatusCode.OK
            };
        }

        public ServiceResult<bool> Delete(int id)
        {
            var subscription = this.context.Subscriptions.Find(id);

            if (subscription != null)
            {
                this.context.Subscriptions.Remove(subscription);
                this.context.SaveChanges();

                return new ServiceResult<bool>()
                {
                    Result = true,
                    Status = System.Net.HttpStatusCode.OK
                };
            }
            else {
                return new ServiceResult<bool>()
                {
                    Message = "User not found",
                    Result = false,
                    Status = System.Net.HttpStatusCode.NotFound
                };

            }
        }
    }
}
