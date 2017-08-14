using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Tic_tac_toe.Models.EF;

namespace Web_Tic_tac_toe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Room> model = new List<Room>(); 

            using (var context = new TttModel())
            {
                model = context.Rooms.Where(r => r.User1 != null).Where(r => r.User2 == null).ToList();
                return View(model);
            }            
        }

        public ActionResult AddRoom()
        {
            return View("Index");            
        }

        [HttpPost]
        public ActionResult AddRoom(int userId)
        {
            using (var context = new TttModel())
            {
                Room room = new Room();
                room.User1 = context.Users.Where(u => u.UserID == userId).First();
                context.Rooms.Add(room);
                context.SaveChanges();

                return View("Index");
            }
        }


    }
}