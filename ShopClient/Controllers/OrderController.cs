using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopClient.Models;
using ShopClient.Models.Clients;

namespace ShopClient.Controllers
{
    public class OrderController : Controller
    {
        private UserClient _userClient;
        private OrderClient _orderClient;
        private LogClient _logClient;

        public OrderController(UserClient userclient, OrderClient orderClient, LogClient logClient)
        {
            _userClient = userclient;
            _orderClient = orderClient;
            _logClient = logClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(string cardNum)
        {
            if (cardNum.Length == 0) {
                return View();
            }
            PaymentMethod p = new PaymentMethod(cardNum);
            User u = _userClient.GetCurrentUser();
            Order order = new Order(u.Id,p);
            order.AddOrderItems(u.GetCart().GetOrderItems());
            u.GetCart().RemoveAll();
            Order nOrder = await _orderClient.CreateOrder(order);
            await _userClient.UpdateAsync(u);
            _logClient.SaveLog(u.Username, u.Username + " create order successfully! The order number is: " + nOrder.Id);
            return View("Success");
        }


        public IActionResult Manage()
        {
            return View();
        }
    }
}