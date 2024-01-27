namespace EventService
{
    public class EventRepository : IEventRepository
    {
        private readonly EventDb _context;

        public EventRepository(EventDb context)
        {
            _context = context;
        }

        public Task<List<Event>> GetEventsAsync() => _context.Events.ToListAsync();

        public async Task<Event> GetEventAsync(int eventId) =>
            await _context.Events.FindAsync(new object[] { eventId });


        public async Task InsertEventAsync(Event _event) => await _context.Events.AddAsync(_event);

        public async Task UpdateEventAsync(Event _event)
        {
            var eventFromDb = await _context.Events.FindAsync(new object[] { _event.Id });
            if (eventFromDb == null) return;
            eventFromDb.Name = _event.Name;
            eventFromDb.Location = _event.Location;
            eventFromDb.Description = _event.Description;
            eventFromDb.Date = _event.Date;            
        }

        public async Task DeleteEventAsync(int eventId)
        {
            var eventFromDb = await _context.Events.FindAsync(new object[] { eventId });
            if (eventFromDb == null) return;
            _context.Events.Remove(eventFromDb);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

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
    }
}

