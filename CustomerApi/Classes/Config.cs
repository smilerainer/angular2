using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using static CustomerApi.Models.Config;

namespace CustomerApi.Classes
{
    public class Config
    {
        public Credentinal Credentials()
        {

            return new Credentinal
            {
                Constring = ConfigurationManager.AppSettings["ConString"]
            };
        }
    }
}