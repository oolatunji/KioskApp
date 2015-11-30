using KioskSolutionLibrary.ModelLibrary;
using KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary.ThirdPartyData;
using KioskSolutionLibrary.ProcessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KioskSolution.Controllers
{
    public class ThirdPartyAPIController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage SavePan([FromBody]PANDetail pan)
        {
            try
            {
                string errMsg = string.Empty;
                bool result = ThirdPartyPL.Save(pan, out errMsg);
                if (string.IsNullOrEmpty(errMsg))
                    return result.Equals(true) ? Request.CreateResponse(HttpStatusCode.OK, "Pan added successfully.") : Request.CreateResponse(HttpStatusCode.BadRequest, "Request failed");
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

        [HttpGet]
        public HttpResponseMessage RetrievePans()
        {
            try
            {
                IEnumerable<Object> pans = ThirdPartyPL.RetrievePans();
                object returnedPans = new { data = pans };
                return Request.CreateResponse(HttpStatusCode.OK, returnedPans);
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
