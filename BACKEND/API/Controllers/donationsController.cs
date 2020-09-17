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
    [RoutePrefix("api/donation")]
    public class donationsController : ApiController
    {
       // [Route("getDonations")]
        //public IHttpActionResult getDonations()
        //{          
        //  return Ok(BL.donationsBL.getDonations());
        //}
    }
}
