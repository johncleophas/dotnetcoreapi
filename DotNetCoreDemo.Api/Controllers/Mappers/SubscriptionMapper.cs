using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Controllers.Mappers
{
    public static class SubscriptionMapper
    {
        public static Data.Subscription MapToService(Subscription controllerSubscription)
        {
            return new Data.Subscription()
            {
                Id = controllerSubscription.Id,
                BookId = controllerSubscription.BookId,
                UserId = controllerSubscription.UserId
            };
        }

        public static Subscription MapToController(Data.Subscription serviceSubscription)
        {
            return new Subscription()
            {
                BookId = serviceSubscription.BookId,
                Id = serviceSubscription.Id,
                UserId = serviceSubscription.UserId
            };
        }

        public static List<Subscription> MapSubscriptionsToController(List<Data.Subscription> serviceSubscriptions)
        {

            var subscriptions = new List<Subscription>();

            foreach (var item in serviceSubscriptions)
            {
                subscriptions.Add(MapToController(item));
            }

            return subscriptions;
        }
    }
}
