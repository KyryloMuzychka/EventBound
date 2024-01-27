namespace EventService
{
	public class Subscription
	{
		public int Id { get; set; }
		public int EventId { get; set; }
		public string SubscriberEmail { get; set; } = string.Empty;
	}
}

