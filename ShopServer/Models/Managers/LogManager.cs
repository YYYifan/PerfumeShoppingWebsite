using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ShopServer.Models.Entities;

namespace ShopServer.Models.Managers
{
    public class LogManager
    {

        public void Save(LogEntity log)
        {
            string fApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
            string dpath = fApplicationPath + "/Log/";
            string filePath = fApplicationPath + "/Log/" + log.id + ".txt";

            bool exists = Directory.Exists(dpath);

            if (!exists)
                Directory.CreateDirectory(dpath);
            if (!File.Exists(filePath))
            {
                FileStream myFile;
                myFile = File.Create(filePath);
                myFile.Close();
            }
            String ss = DateTime.Now.ToString() + " : " + log.log + "\n";
            File.AppendAllText(filePath, ss);
        }
    }
}
