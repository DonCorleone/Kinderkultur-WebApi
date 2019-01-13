using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace KinderKulturServer.SignalRHubs
{
   public class ChatHub : Hub
   {

      public async Task NewMessage(string username, string message)
      {
         await Clients.All.SendAsync("messageReceived", username, message);
      }

      public Task Send(string data)
      {
         return Clients.All.SendAsync("Send", this.Context.ConnectionId + ": "  + data);
      }
   }
}