using System;
using System.Threading.Tasks;
using Mozu.Api;
using Mozu.Api.Contracts.Event;
using Mozu.Api.Events;

namespace MozuDataConnector.Domain.Handlers
{
    public class OrderEventHandler : IOrderEvents
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

        public Task OpenedAsync(IApiContext apiContext, Event eventPayLoad)
        {
            throw new NotImplementedException();
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
