using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace KinderKulturServer.Hubs
{
   public class ChatHub : Hub
   {
      // public async Task Send(string user, string message)
      // {
      //    await base.Clients.Others.SendAsync("Send", user, message);
      // }

      public async Task NewMessage(string username, string message)
      {
         await Clients.All.SendAsync("messageReceived", username, message);
      }

      public Task Send(string data)
      {
         return Clients.All.SendAsync("Send", data);
      }
   }
}