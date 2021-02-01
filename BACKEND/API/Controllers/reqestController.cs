using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/reqest")]
    public class reqestController : ApiController
    {
        [Route("addReqest"), HttpPost]
        public IHttpActionResult addReqest(DTO.RequestForLoan l)
        {
            System.Diagnostics.Debug.WriteLine(l.comment);
            return Ok(BL.reqestBL.addReqest(l));
        }
    }
}