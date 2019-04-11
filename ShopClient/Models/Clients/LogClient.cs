using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShopServer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ShopClient.Models.Clients
{
    public class LogClient
    {
        private Uri _hostUri;

        public LogClient(IConfiguration config)
        {
            var configString = config.GetSection("ClientConnectionStrings")["Server"];
            _hostUri = new Uri(configString);
        }

        public System.Net.HttpStatusCode SaveLog(string id,string content)
        {
            LogEntity log = new LogEntity(content, id);
            var client = new HttpClient();
            client.BaseAddress = new Uri(_hostUri, "api/Log/SaveLog");
            using (client)
            {
                HttpResponseMessage response = null;
                try
                {
                    var output = JsonConvert.SerializeObject(log);
                    HttpContent contentPost = new StringContent(output, System.Text.Encoding.UTF8, "application/json");
                    response = client.PostAsync(client.BaseAddress, contentPost).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return response.StatusCode;
            }
        }
    }
}
