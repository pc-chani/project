using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/searches")]
    public class SearchesController : ApiController
    {
        [Route("AddSearch"), HttpPost]

        public IHttpActionResult AddSearch(DTO.searches s)
        {

            return Ok(BL.searchesBL.addSearch(s));
        }
    }
}
