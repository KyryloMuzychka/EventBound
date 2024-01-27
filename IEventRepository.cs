namespace EventService
{
	public interface IEventRepository : IDisposable
	{
        Task<List<Event>> GetEventsAsync();
        Task<Event> GetEventAsync(int eventId);
        Task InsertEventAsync(Event _event);
        Task UpdateEventAsync(Event _event);
        Task DeleteEventAsync(int eventId);
        Task SaveAsync();
    }
}