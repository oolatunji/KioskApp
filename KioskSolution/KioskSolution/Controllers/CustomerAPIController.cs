using KioskSolution.Models;
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

        [HttpPost]
        public HttpResponseMessage SentTokenToCustomer([FromBody]TokenModel tokenRequest)
        {
            try
            {
                dynamic returnedCustomer = CustomerPL.SendTokenToCustomer(tokenRequest.AccountNumber, tokenRequest.SerialNumber);
                object customerToken = new { data = returnedCustomer };
                return Request.CreateResponse(HttpStatusCode.OK, customerToken);
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                response.ReasonPhrase = ex.Message;
                return response;
            }
        }

        [HttpPost]
        public HttpResponseMessage SaveCustomerCardRequest([FromBody]CardRequestModel cardRequestModel)
        {
            try
            {
                CardRequest cardRequest = new CardRequest();
                cardRequest.CustomerID = cardRequestModel.CustomerID;
                cardRequest.CardType = Enum.GetName(typeof(StatusUtil.CardType), cardRequestModel.CardTypeID);
                cardRequest.RequestType = Enum.GetName(typeof(StatusUtil.RequestType), cardRequestModel.RequestTypeID);
                cardRequest.PickupBranchID = cardRequestModel.PickupBranchID;
                cardRequest.Status = StatusUtil.CardStatus.Requested.ToString();
                cardRequest.ModifiedDate = System.DateTime.Now;
                cardRequest.SerialNumber = cardRequestModel.SerialNumber;

                bool result = CustomerPL.SaveCardRequest(cardRequest);

                return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Successful") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
            }
            catch (Exception ex)
            {
                ErrorHandler.WriteError(ex);
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }

        [HttpGet]
        public HttpResponseMessage RetrieveCardRequests()
        {
            try
            {
                IEnumerable<Object> cardRequests = CustomerPL.RetrieveCardRequests();
                object returnedCardRequests = new { data = cardRequests };
                return Request.CreateResponse(HttpStatusCode.OK, returnedCardRequests);
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
