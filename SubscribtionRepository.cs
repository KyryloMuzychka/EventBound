using EventService;

namespace EventBound
{
    public class SubscribtionRepository : ISubscribtionRepository
    {
        private readonly EventDb _context;

        public SubscribtionRepository(EventDb context)
        {
            _context = context;
        }

        public Task<List<Subscription>> GetSubscribtionAsync() => _context.Subscriptions.ToListAsync();

        public async Task<Subscription> GetSubscribtionAsync(int subscriptionId) =>
            await _context.Subscriptions.FindAsync(new object[] { subscriptionId });

       
        public async Task InsertSubscribtionAsync(Subscription subscription) => await _context.Subscriptions.AddAsync(subscription);

        async Task ISubscribtionRepository.SaveAsync() => await _context.SaveChangesAsync();

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _disposed = disposing;
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            var subscriptionFromDB = await _context.Subscriptions.FindAsync(new object[] { subscription.Id });
            if (subscriptionFromDB == null) return;
            subscriptionFromDB.EventId = subscription.EventId;
            subscriptionFromDB.SubscriberEmail = subscription.SubscriberEmail;                        
        }

        public async Task DeleteSubscriptionAsync(int subscriptionId)
        {
            var subscriptionFromDb = await _context.Subscriptions.FindAsync(new object[] { subscriptionId });
            if (subscriptionFromDb == null) return;
            _context.Subscriptions.Remove(subscriptionFromDb);
        }
    }
}

