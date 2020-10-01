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
    [RoutePrefix("api/gmh")]
    public class gmhController : ApiController
    {
        [Route("add"), HttpPost]
        public IHttpActionResult add(DTO.GMH gMH)
        {
            return Ok(BL.gmhBL.addGMH(gMH));
   
        }
        [Route("getMyGmhim"), HttpPost]
        public IHttpActionResult getMyGmhim(DTO.User user)
        {
            return Ok(BL.gmhBL.getMyGmhim(user));      
        }
        [Route("delete"), HttpPost]
        public IHttpActionResult delete(DTO.GMH gMH)
        {
            return Ok(BL.gmhBL.delete(gMH));
        }
        [Route("saveChange"), HttpPost]
        public IHttpActionResult saveChange(DTO.GMH gMH)
        {
            return Ok(BL.gmhBL.saveChange(gMH));
        }
    }
    
}
