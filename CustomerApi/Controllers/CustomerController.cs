using Crud.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using static CustomerApi.Models.CustomerModel;

namespace CustomerApi.Controllers
{
    //[Authenticate]
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        DapperTransaction dapper = new DapperTransaction();


        [HttpGet]
        [Route("GetCustomerList")]//     api/GetCustomerList
        public IEnumerable<Customer> GetCustomerList()
        {
            return dapper.GetCustomerList();
        }


        [HttpPost]
        [Route("UpdateCustomer")]  // PUT api/students/UpdateCustomer          with attached json
        public IHttpActionResult UpdateCustomer( Customer updatedProduct)
        {
            List<Customer> cusLIst = dapper.GetCustomerList();


            var product = cusLIst.FirstOrDefault(p => p.CustomerNo == updatedProduct.CustomerNo);
            if (product == null)
                return NotFound();


            dapper.UpdateData(updatedProduct);

            return Ok(product);
        }

        [HttpPost]
        [Route("AddCustomer")] //     api/AddCustomer         with attached json
        public IHttpActionResult AddCustomer(Customer product)
        {
           // customers.Add(product);

            dapper.InsertData(product);
            return Ok(product);
        }

        [HttpPost]
        [Route("DeleteCustomer")] //     api/AddCustomer         with attached json
        public IHttpActionResult DeleteCustomer(string CustomerID)
        {
            foreach (var cusID in CustomerID.Split(','))
            {
                dapper.DeleteData(cusID);
            }
        
            return Ok();
        }


        //[HttpGet]
        //[Route("{id:int}")] //     api/customer/1
        //public IHttpActionResult GetCustomerByID(int id)
        //{
        //    var product = customers.FirstOrDefault(p => p.ID == id);
        //    if (product == null)
        //        return NotFound();

        //    return Ok(product);
        //}


        [HttpGet]
        [Route("GetCustomerByCusID")]//      api/customer/GetCustomerByCusID?cusNo=c-111
        public IHttpActionResult GetCustomerByCusNo(string cusNo)
        {
            var customer = dapper.GetUserById(cusNo);
            if (customer == null)
                return NotFound();

            return Ok(customer);


        }


        [HttpGet]
        [Route("GetCustomerByCusID/{customerId:int}")] //    api/customer/GetCustomerByCusID/1
        public IHttpActionResult GetCustomerByCusID(string customerId)
        {


            var customer = dapper.GetUserById(customerId);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }




    }
}

