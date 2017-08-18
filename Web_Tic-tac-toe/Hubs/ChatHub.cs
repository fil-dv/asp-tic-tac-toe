using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Web_Tic_tac_toe.Models.EF;

namespace WebSignalRFirst.Hubs
{
    public class ChatHub : Hub
    {
        static List<User> Users = new List<User>();        

        // Отправка сообщений всем юзерам
        public void Send(string name, string message)
        {
            string currentTime = DateTime.Now.ToString("hh:mm:ss");
            Clients.All.addMessage(currentTime, name, message); //addMessage - метод объявляется на стороне клиента в коде javascript. 
        }
        // Подключение нового пользователя
        public void Connect(string userName)
        {
            using (var efContext = new DbTTTEntities())
            {
                Users = efContext.Users.ToList();
                
               // if (Session["userID"] != null)
                {
                   // User currentUser = Users.First(u => u.UserID == (int)Session["userID"]);

                    var id = Context.ConnectionId;
                    if (!Users.Any(x => x.ChatConnectID == id))
                    {
                        //Users.Add(new ChatUser { ConnectionId = id, Name = userName });
                        // Посылаем сообщение текущему пользователю
                        Clients.Caller.onConnected(id, userName, Users);
                        // Посылаем сообщение всем пользователям, кроме текущего
                        Clients.AllExcept(id).onNewUserConnected(id, userName);
                    }
                }
            }
        }
        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ChatConnectID == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}