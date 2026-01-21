using MassTransit;
using Contracts.Events;

namespace Notifications.API.Consumers;

public sealed class PaymentProcessedEventConsumer : IConsumer<PaymentProcessedEvent>
{
    public Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        var msg = context.Message;

        if (msg.Status == PaymentStatus.Approved)
        {
            Console.WriteLine($"[Notifications] Purchase confirmation email (simulated) => UserId={msg.UserId} | GameId={msg.GameId} | Price={msg.Price}");
        }
        else
        {
            Console.WriteLine($"[Notifications] Payment rejected (simulated) => UserId={msg.UserId} | GameId={msg.GameId} | Price={msg.Price}");
        }

        return Task.CompletedTask;
    }
}
