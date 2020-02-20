using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Sally.Provider;
using Sally.Interfaces;
using Autofac;
using SallyProviderExample.DialogManagers;

namespace SallyProviderExample.Controllers
{

    [RoutePrefix("api/TalkUser/HowAreYou")]
    [AllowAnonymous]
    public class TalkUser_HowAreYou_Controller : ApiController
    {

        [Route("PostDialog")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostDialog([FromBody] ExternalFunctionExecutableRequest ExternalRequest)
        {
            try
            {
                //INIT
                TalkUser_HowAreYou_DialogManager Manager = SallyProviderContainer.ApplicationContainer.Resolve<TalkUser_HowAreYou_DialogManager>();
                SallyProviderRequestContext RequestContext = SallyProviderRequestContext.GetContext(ExternalRequest);

                //Run Functions
                ExternalFunctionExecutableResponse Response = await Manager.PostDialog(RequestContext);

                //Return Result
                return Request.CreateResponse(HttpStatusCode.OK, Response);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }

        }

    }
}