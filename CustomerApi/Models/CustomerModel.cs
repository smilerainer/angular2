using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerApi.Models
{
    public class CustomerModel
    {
        public class Customer
        {
            public int ID { get; set; }
            public string CustomerNo { get; set; }
            public string Name { get; set; }
            public string Contact { get; set; }
        }
    }
}