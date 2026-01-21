using MassTransit;
using Contracts.Events;

namespace Notifications.API.Consumers;

public sealed class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
{
    public Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var msg = context.Message;
        Console.WriteLine($"[Notifications] Welcome email (simulated) => To={msg.Email} | Name={msg.Name} | UserId={msg.UserId}");
        return Task.CompletedTask;
    }
}
