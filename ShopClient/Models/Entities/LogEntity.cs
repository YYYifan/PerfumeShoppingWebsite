using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.Models.Entities
{
    public class LogEntity
    {
        public string log;
        public string id;

        public LogEntity(string log, string id)
        {
            this.log = log;
            this.id = id;
        }
    }
}
