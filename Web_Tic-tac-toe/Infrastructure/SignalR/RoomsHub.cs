using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Web_Tic_tac_toe.Models.EF;
using Microsoft.AspNet.SignalR.Hubs;

namespace Web_Tic_tac_toe.Infrastructure.SignalR
{
    [HubName ("HubOfRooms")]
    public class RoomsHub : Hub
    {
        public void SendRooms(List<Room> roomList)
        {
            Clients.All.broadcastMessage(roomList);
        }
    }
}