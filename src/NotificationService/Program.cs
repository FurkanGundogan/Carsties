using MassTransit;
using NotificationService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x=>
{

    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("nt",false));

    x.UsingRabbitMq((context,cfg)=>{

        cfg.UseRetry(r => {
            r.Handle<RabbitMqConnectionException>();
            r.Interval(5, TimeSpan.FromSeconds(10));
        });
        
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username","guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password","guest"));
        });
        
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddSignalR();

builder.Services.AddCors(options => {
    options.AddPolicy("customPolicy", b=> {
            b.WithOrigins(builder.Configuration["ClientApp"], "https://app.carsties.com/") // Sadece belirli bir origin için izin vermek
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Kimlik bilgileri ile istek gönderen istemcilere izin vermek
    });
});

var app = builder.Build();

 app.UseCors("customPolicy");

app.MapHub<NotificationHub>("/notifications");


app.Run();
