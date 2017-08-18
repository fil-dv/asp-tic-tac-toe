using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Web_Tic_tac_toe.Models.EF;
using Microsoft.AspNet.SignalR.Hubs;

namespace Web_Tic_tac_toe.Hubs
{
    [HubName ("myHub")]
    public class RoomsHub : Hub
    {
        public void SendRooms(/*List<Room> roomList*/string str)
        {

            var context = GlobalHost.ConnectionManager.GetHubContext<RoomsHub>();
            context.Clients.All.broadcastMessage(str);


           
            //Clients.All.broadcastMessage(roomList);
            //Clients.All.broadcastMessage(str);
        }
    }
}