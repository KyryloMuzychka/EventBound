namespace EventService
{
	public class EventDb : DbContext
	{
        public DbSet<Event> Events => Set<Event>();
		public DbSet<Subscription> Subscriptions => Set<Subscription>();

        public EventDb(DbContextOptions<EventDb> options) : base(options) { }
    }
}