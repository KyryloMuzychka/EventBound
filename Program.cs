var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EventDb>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ISubscribtionRepository, SubscribtionRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<EventDb>();
    db.Database.EnsureCreated();
}




app.MapGet("/events", async (IEventRepository repositry) =>
    Results.Ok(await repositry.GetEventsAsync()));

app.MapGet("/events/{id}", async (int Id, IEventRepository repository) =>
    await repository.GetEventAsync(Id) is Event _event
    ? Results.Ok(_event)
    : Results.NotFound());

app.MapPost("/events", async ([FromBody] Event _event, IEventRepository repository) =>
{
    await repository.InsertEventAsync(_event);
    await repository.SaveAsync();
    return Results.Created($"/events/{_event.Id}", _event);
});

app.MapPut("/events", async ([FromBody] Event _event, IEventRepository repository) =>
{
    await repository.UpdateEventAsync(_event);
    await repository.SaveAsync();
    return Results.NoContent();
});

app.MapDelete("/events/{id}", async (int Id, IEventRepository repository) => {
    await repository.DeleteEventAsync(Id);
    await repository.SaveAsync();
    return Results.NoContent();
});




app.MapGet("/subscriptions", async (ISubscribtionRepository repositry) =>
    Results.Ok(await repositry.GetSubscribtionAsync()));

app.MapGet("/subscriptions/{id}", async (int Id, ISubscribtionRepository repository) =>
    await repository.GetSubscribtionAsync(Id) is Subscription subscription
    ? Results.Ok(subscription)
    : Results.NotFound());

app.MapPost("/subscriptions", async ([FromBody] Subscription subscription, ISubscribtionRepository repository) =>
{
    await repository.InsertSubscribtionAsync(subscription);
    await repository.SaveAsync();
    return Results.Created($"/subscriptions/{subscription.Id}", subscription);
});

app.MapPut("/subscriptions", async ([FromBody] Subscription subscription, ISubscribtionRepository repository) =>
{
    await repository.UpdateSubscriptionAsync(subscription);
    await repository.SaveAsync();
    return Results.NoContent();
});

app.MapDelete("/subscriptions/{id}", async (int Id, ISubscribtionRepository repository) => {
    await repository.DeleteSubscriptionAsync(Id);
    await repository.SaveAsync();
    return Results.NoContent();
});




app.UseHttpsRedirection();

app.Run();