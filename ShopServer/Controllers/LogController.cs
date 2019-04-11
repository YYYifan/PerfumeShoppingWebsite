using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopServer.Models;
using ShopServer.Models.Entities;
using ShopServer.Models.Managers;

namespace ShopServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : Controller
    {
        private readonly LogManager _logService;

        public LogController(LogManager logService)
        {
            _logService = logService;
        }

        [HttpPost]
        [Route("[action]")]
        [ActionName("SaveLog")]
        public StatusCodeResult SaveLog([FromBody]LogEntity log)
        { 
            _logService.Save(log);
            return new StatusCodeResult(200);
            
        }
    }
}