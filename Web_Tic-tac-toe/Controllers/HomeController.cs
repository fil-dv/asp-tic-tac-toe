using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Tic_tac_toe.Infrastructure.SignalR;
using Web_Tic_tac_toe.Models.EF;

namespace Web_Tic_tac_toe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // List<Room> model = new List<Room>(); 

            RoomsHub hub = new RoomsHub();  
            hub.SendRooms("from controller");


            var context = new DbTTTEntities();
            List<Room> model = context.Rooms.Where(r => r.User1 != null).Where(r => r.User2 == null).ToList();
            return View(model);

            //using (var context = new TttModel())
            //{
            //    List<Room> model = context.Rooms.Where(r => r.User1 != null).Where(r => r.User2 == null).ToList();
            //    ////List<Room> model1 = (from r in context.Rooms where (r.User1 != null && r.User2 == null)
            //    ////                     select new Room
            //    ////                     {
            //    ////                         RoomID = r.RoomID,
            //    ////                         GameID = r.GameID,
            //    ////                         User_1 = r.User1.UserID,
            //    ////                         User_2 = r.User2.UserID
            //    ////                     }).ToList();

            //    return View(model);
            //}            
        }


        public ActionResult AddRoom()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            using (var context = new DbTTTEntities())
            {
                Room room = new Room();
                int userId = (int)Session["userID"];
                List<User> users = context.Users.ToList();
                User user = users.Where(u => u.UserID == userId).First();
                room.User1 = user;
                context.Rooms.Add(room);
                context.SaveChanges();

                return View("Index");
            }
        }

        //[HttpPost]
        //public ActionResult AddRoom(int userId)
        //{
            
        //}


    }
}