using Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace NotificationService;

public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public BidPlacedConsumer(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("--> bid placed message received");
        Console.WriteLine("--> Id" + context.Message.Id);
        Console.WriteLine("--> AuctionId" + context.Message.AuctionId);
        Console.WriteLine("--> Bidder" + context.Message.Bidder);
        Console.WriteLine("--> Amount" + context.Message.Amount);

        await _hubContext.Clients.All.SendAsync("BidPlaced", context.Message);

    }
}
