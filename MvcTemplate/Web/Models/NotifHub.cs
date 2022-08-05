using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Web.Models
{
    public class NotifHub : Hub
    {
        public async Task SendNotification(string sender, string reciever, string notifTitle, string body)
        {
            await Clients.All.SendAsync("RecieveMessage", sender, reciever, notifTitle, body);
        }
    }
}
