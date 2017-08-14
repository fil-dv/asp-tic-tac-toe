using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Tic_tac_toe.Models.AccountModels;
using Web_Tic_tac_toe.Models.EF;



namespace Web_UI.Controllers
{
    public class AccountController : Controller
    {
        
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new TttModel())
                {
                    List<User> users = context.Users.Where(u => u.UserEmail == model.Email).ToList();
                    if (users.Count > 0)
                    {
                        ViewBag.AlreadyUsed = "Пользователь с таким адресом уже зарегистрирован.";
                        return View();
                    }
                    else
                    {
                        User newUser = new User();
                        newUser.UserEmail = model.Email;
                        newUser.UserPass = model.Password.GetHashCode().ToString();
                        context.Users.Add(newUser);
                        context.SaveChanges();
                        TempData["registerSuccess"] = "Регистрация прошла успешно.";
                    }
                }                
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(); 
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new TttModel())
                {
                    List<User> users = context.Users.Where(u => u.UserEmail == model.Email).ToList();
                    if (users.Count > 0)
                    {
                        if (users[0].UserPass == model.Password.GetHashCode().ToString())
                        {
                            Session["isLogIn"] = true;
                            Session["userName"] = users[0].UserEmail;
                            Session["userID"] = users[0].UserID;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.LoginError = "Не верный пароль.";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.LoginError = "Не верный email.";
                        return View();
                    }                   
                }
            }
            else
            {
                return View();
            }                   
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session["isLogIn"] = false;
            Session["userName"] = null;
            Session["userID"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AboutUser()
        {
            if (Session["userID"] != null)
            {
                using (var context = new TttModel())
                {
                    int id;
                    if (Int32.TryParse(Session["userID"].ToString(), out id))
                    {
                        var user = context.Users.Where(u => u.UserID == id).First();
                        LoginModel model = new LoginModel { Email = user.UserEmail };
                        return View(model);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            if (Session["isLogIn"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
           
            if (Session["userID"] != null)
            {
                using (var context = new TttModel())
                {
                    int id;
                    if (Int32.TryParse(Session["userID"].ToString(), out id))
                    {
                        var user = context.Users.Where(u => u.UserID == id).First();
                        if (model.OldPassword.GetHashCode().ToString() == user.UserPass)
                        {
                            user.UserPass = model.Password.GetHashCode().ToString();
                            context.SaveChanges();
                            TempData["passwordChange"] = "Пароль успешно изменен.";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.ChangeError = "Не верный старый пароль.";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.ChangeError = "Не удалось изменить пароль.";
                        return View();
                    }
                }
            }
            else
            {
                ViewBag.ChangeError = "Не удалось изменить пароль.";
                return View();
            }            
        }        
    }
}