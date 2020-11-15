using DotNetCoreDemo.Api.Controllers.Models;
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

        public SubscriptionService(AppSettings appSettings)
        {
            this.context = new DataContext(appSettings);
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

        public ServiceResult<List<UserBookSubscription>> GetSubscribedBooksByUser(string userName)
        {
            try
            {
                var user = this.context.Users.FirstOrDefault(e => e.UserName == userName);

                if (user == null)
                {
                    throw new ArgumentException($"Username {userName} not found");
                }

                var activeSubscriptions = this.context.Subscriptions.Where(e => e.UserId == user.Id).ToList();

                var allBooks = this.context.Books;

                var subs = new List<UserBookSubscription>();

                foreach (var item in activeSubscriptions)
                {
                    var book = allBooks.SingleOrDefault(e=>e.Id == item.BookId);

                        subs.Add(new UserBookSubscription()
                        {
                            BookId = book.Id,
                            Name = book.Name,
                            Price = book.Price,
                            SubscriptionId = item.Id,
                            Text = book.Text,
                            Username = user.UserName
                        });
                }

                return new ServiceResult<List<UserBookSubscription>>()
                {
                    Result = subs,
                    Status = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception)
            {
                return new ServiceResult<List<UserBookSubscription>>()
                {
                    Result = null,
                    Message = "Unable to return results",
                    Status = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public ServiceResult<List<UserBookSubscription>> GetAvailableBooksByUser(string userName)
        {
            try
            {
                var user = this.context.Users.FirstOrDefault(e => e.UserName == userName);

                if (user == null)
                {
                    throw new ArgumentException($"Username {userName} not found");
                }

                var activeSubscriptions = this.context.Subscriptions.Where(e => e.UserId == user.Id).ToList();

                var allBooks = this.context.Books;

                var availableSubs = new List<UserBookSubscription>();

                foreach (var item in allBooks)
                {
                    if (!activeSubscriptions.Select(e => e.BookId).Contains(item.Id)){
                        availableSubs.Add(new UserBookSubscription()
                        {
                            BookId = item.Id,
                            Name = item.Name,
                            Price = item.Price,
                            SubscriptionId = 0,
                            Text = item.Text,
                            Username = user.UserName
                        });
                    }
                }

                return new ServiceResult<List<UserBookSubscription>>()
                {
                    Result = availableSubs,
                    Status = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception)
            {
                return new ServiceResult<List<UserBookSubscription>>()
                {
                    Result = null,
                    Message = "Unable to return results",
                    Status = System.Net.HttpStatusCode.InternalServerError
                };
            }
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
            else
            {
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
