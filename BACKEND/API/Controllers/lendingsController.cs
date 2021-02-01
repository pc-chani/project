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
    [RoutePrefix("api/lendings")]
    public class lendingsController : ApiController
    {
        [Route("addLending"), HttpPost]
        public IHttpActionResult addLending(DTO.LENDING l)
        {
            
            return Ok(BL.lendingsBL.addLending(l));
        }
        [Route("getLendings"), HttpPost]
        public IHttpActionResult getLendings(DTO.ProductToGMH p)
        {
            return Ok(BL.lendingsBL.getLendings(p));
        }
        [Route("deleteLending"), HttpPost]
        public IHttpActionResult deleteLending(DTO.LENDING l)
        {
            return Ok(BL.lendingsBL.deleteLending(l));
        }
    }
}
