using MassTransit;
using Notifications.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

var enableRabbitMq = builder.Configuration.GetValue<bool>("Messaging:EnableRabbitMQ", true);

if (enableRabbitMq)
{
    builder.Services.AddMassTransit(x =>
    {
        x.AddConsumer<UserCreatedEventConsumer>();
        x.AddConsumer<PaymentProcessedEventConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
            var host = builder.Configuration["RabbitMQ:Host"] ?? "rabbitmq";
            cfg.Host(host, "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            cfg.ReceiveEndpoint("user-created-queue", e =>
            {
                e.ConfigureConsumer<UserCreatedEventConsumer>(context);
            });

            cfg.ReceiveEndpoint("payment-processed-queue-notifications", e =>
            {
                e.ConfigureConsumer<PaymentProcessedEventConsumer>(context);
            });
        });
    });
}

var app = builder.Build();
app.MapGet("/api/v1/health", () => Results.Ok(new { status = "ok" }));
app.Run();
