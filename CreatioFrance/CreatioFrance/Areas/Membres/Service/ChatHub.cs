using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
namespace CreatioFrance.Areas.Membres.Controllers
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients. 
            Clients.All.addNewMessageToPage(name, message);

            //// Create a Long running task to do an infinite loop which will keep sending the server time
            //// to the clients every 3 seconds.
            //var taskTimer = Task.Factory.StartNew(async () =>
            //{
            //    while (true)
            //    {
            //        string timeNow = DateTime.Now.ToString();
            //        //Sending the server time to all the connected clients on the client method SendServerTime()
            //        Clients.All.SendServerTime(timeNow);
            //        //Delaying by 3 seconds.
            //        await Task.Delay(3000);
            //    }
            //}, TaskCreationOptions.LongRunning
            //    );
        }
    }
}