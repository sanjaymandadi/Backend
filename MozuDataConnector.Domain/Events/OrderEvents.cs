using System;
using System.Threading.Tasks;
using Mozu.Api;
using Mozu.Api.Contracts.Event;
using Mozu.Api.Events;
using Mozu.Api.Resources.Commerce;
using Mozu.Api.Resources.Commerce.Catalog.Admin.Attributedefinition;

namespace MozuDataConnector.Domain.Events
{
    public class OrderEvents : IOrderEvents
    {
        public void Abandoned(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public Task AbandonedAsync(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public void Cancelled(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public Task CancelledAsync(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public void Closed(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public Task ClosedAsync(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public void Fulfilled(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public Task FulfilledAsync(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public void Opened(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public async Task OpenedAsync(IApiContext apiContext, Event eventPayLoad)
        {
            var orderId = eventPayLoad.EntityId;

            var orderResource = new OrderResource(apiContext);
            var order = await orderResource.GetOrderAsync(orderId);

            foreach (var orderItem in order.Items)
            {
                var productCode = orderItem.Product.Properties;
            }
        }

        public void PendingReview(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public Task PendingReviewAsync(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public void Updated(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }

        public Task UpdatedAsync(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
        }
    }
}
