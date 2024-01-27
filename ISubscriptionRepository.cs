namespace EventBound
{
	public interface ISubscribtionRepository : IDisposable
    {
        Task<List<Subscription>> GetSubscribtionAsync();       
        Task<Subscription> GetSubscribtionAsync(int subscriptionId);
        Task InsertSubscribtionAsync(Subscription subscription);
        Task UpdateSubscriptionAsync(Subscription subscription);
        Task DeleteSubscriptionAsync(int subscriptionId);
        Task SaveAsync();
    }
}

