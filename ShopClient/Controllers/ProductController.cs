using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopClient.Models;
using ShopClient.Models.Clients;
using ShopClient.Models.ViewModels;

namespace ShopClient.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private ProductClient _productClient;
        private LogClient _logClient;
        public ProductController(ProductClient productClient, LogClient logClient) {
            _productClient = productClient;
            _logClient = logClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductViewModel model) {
            string photo;
            switch (model.Type)
            {
                case Product.ProductType.SeaSalt:
                    photo = "SeaSalt.jpg";
                    break;
                case Product.ProductType.Freesia:
                    photo = "Freesia.jpg";
                    break;
                case Product.ProductType.WildBlueBell:
                    photo = "WildBlueBell.jpg";
                    break;
                case Product.ProductType.Peony:
                    photo = "Peony.jpg";
                    break;
                default:
                    photo = "SeaSalt.jpg";
                    break;
            }
            Product product = new Product {Name= model.Name, Price = model.Price,Photo= photo, Type = model.Type, Description =model.Description };
            _productClient.CreateProduct(product);
            WriteProductsList(product);
            _logClient.SaveLog("Products", "Product "+product.Name+" has been added to perfume shop successfully!");
            return RedirectToAction("Index", "Home");
        }

        static void WriteProductsList(Product product)
        {
                var output = JsonConvert.SerializeObject(product);
               System.Diagnostics.Debug.WriteLine(output);
        }
    }
}