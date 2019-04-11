using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopClient.Models;
using ShopClient.Models.Clients;
using ShopClient.Models.ViewModels;

namespace ShopClient.Controllers
{   [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private UserClient _userClient;
        private LogClient _logClient;

        public AccountController(UserClient userclient, LogClient logClient)
        {
            _userClient = userclient;
            _logClient = logClient;
        }


        // login method for the user login 
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            LoginEntity loginInfo = new LoginEntity(model.Email, model.Password);
            User user = await _userClient.LoginAsync(loginInfo);
            if (null == user) {
                return View("LoginError");
            }
            _logClient.SaveLog(user.Username, user.Username + " login successfully!");
            return RedirectToAction("Index", "Home");
        }

        // register method for the user to register 
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            User user = new User(model.Username, model.Email, model.Password);
            string name = (await _userClient.RegisterAsync(user)).Username;
            User u = _userClient.GetCurrentUser();
            _logClient.SaveLog(u.Username, u.Username + " register and login successfully!");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View("Login");
        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            User user = _userClient.GetCurrentUser();
            _userClient.Logout();
            _logClient.SaveLog(user.Username, user.Username + " logout successfully!");
            return RedirectToAction("Index", "Home");
        }

    }
}