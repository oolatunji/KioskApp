using KioskSolutionLibrary.ModelLibrary;
using KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary;
using KioskSolutionLibrary.ProcessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KioskSolution.Controllers
{
    public class CustomerAPIController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SaveCustomer([FromBody]Customer customer)
        {
            try
            {
                string errMsg = string.Empty;
                Random rand = new Random();
                string accountnumber = rand.Next(20000, 39999).ToString();
                rand = new Random();
                accountnumber += rand.Next(10000, 99999).ToString();

                customer.AccountNumber = accountnumber;
                bool result = CustomerPL.Save(customer, out errMsg);
                if (string.IsNullOrEmpty(errMsg))
                    return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Customer added successfully.") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.BadRequest, errMsg);
                    return response;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateCustomer([FromBody]Customer customer)
        {
            try
            {
                bool result = CustomerPL.Update(customer);
                return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Customer updated successfully") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        [HttpGet]
        public HttpResponseMessage RetrieveCustomers()
        {
            try
            {
                IEnumerable<Object> customers = CustomerPL.RetrieveCustomers();
                object returnedCustomers = new { data = customers };
                return Request.CreateResponse(HttpStatusCode.OK, returnedCustomers);
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.ReasonPhrase = ex.Message;
                return response;
            }
        }
    }
}
