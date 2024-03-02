using Microsoft.AspNetCore.SignalR;

namespace NotificationService;

public class NotificationHub : Hub
{

    public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var negotiateUrl = $"http://api.carsties.com/notifications/negotiate?negotiateVersion=1&connectionId={connectionId}";

            using (var httpClient = new HttpClient())
            {
                await httpClient.GetAsync(negotiateUrl);
            }

            await base.OnConnectedAsync();
        }

}
