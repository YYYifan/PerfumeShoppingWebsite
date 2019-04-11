using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopClient.Models;
using ShopClient.Models.Clients;

namespace ShopClient.Controllers
{
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        private UserClient _userClient;
        private ProductClient _productClient;
        private CartEntity _cart;
        private LogClient _logClient;

        public CartController(UserClient userClient, ProductClient productClient, LogClient logClient) {
            _userClient = userClient;
            _productClient = productClient;
            _cart = userClient.GetCurrentUser().GetCart();
            _logClient = logClient;
        }

        public IActionResult Index()
        {
           // System.Diagnostics.Debug.WriteLine("id ===" + id);
            return View();
        }

        public async Task<IActionResult> AddAsync(string id)
        {
            Product p = _productClient.GetProductById(id);
            _cart.Add(p.Id,p.Name, p.Price,p.Photo);
            if (_userClient.IsSignedIn()) {
                await _userClient.UpdateAsync(_userClient.GetCurrentUser());
                User user = _userClient.GetCurrentUser();
                _logClient.SaveLog(user.Username, user.Username + " add product " + p.Name + " to cart successfully!");
                return RedirectToAction("Index", "Cart");
            }
            else {
                return RedirectToAction("Login", "Account");
            }
            
        }

        [Route("RemoveAsync/{id}")]
        public async Task<IActionResult> RemoveAsync(string id)
        {

            System.Diagnostics.Debug.WriteLine(id);
            _cart.Remove(id);
            await _userClient.UpdateAsync(_userClient.GetCurrentUser());
            User user = _userClient.GetCurrentUser();
            Product p = _productClient.GetProductById(id);
            _logClient.SaveLog(user.Username, user.Username + " remove product " + p.Name + " from cart successfully!");
            return RedirectToAction("Index", "Cart");
        }


        public IActionResult SelectPaymentMethod()
        {
            return View();
        }
    }
}