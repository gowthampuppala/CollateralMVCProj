using CollateralMVC.Data;
using CollateralMVC.Models;
using CollateralMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollateralMVC.Controllers
{
    public class LoginController : Controller
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CollateralController));

        private IUserService _userService;
        private readonly UserDbContext _db;
        public LoginController(IUserService userService, UserDbContext db)
        {
            _userService = userService;
            _db = db;
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Logger(Login login)
        {
            _log4net.Info("Login Intiated");
            List<string> s = new List<string>();
            string pass = _userService.EncodePassword(login.Password);
            User user = _db.Users.Where(s => s.Email == login.Email).FirstOrDefault();
            if (user == null)
            {
                _log4net.Error("No user Found");
                ViewBag.Message = string.Format("No Admin User Found");
                return View();
            }
            else
            {
                if (pass != user.Password)
                {
                    _log4net.Info("Password Wrong");
                        
                        ViewBag.Message = string.Format("password is incorrect");
                        return View();
                    
                }
                else
                {
                    var token = _userService.Login(login.Email, pass);

                    if (token == null || token == String.Empty)
                    {       
                            ViewBag.Message = string.Format("User name or password is incorrect");
                            return View();
                    }
                    Response.Cookies.Append("token", token, new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        HttpOnly = true
                    });
                    _log4net.Info("Login Successful");
                    s.Add(token);
                    return RedirectToAction("", "Loan");
                    /*ViewBag.Message = string.Format("Login successfull");
                    return View();*/
                }
            }
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }        
    }
}
