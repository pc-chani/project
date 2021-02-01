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
    [RoutePrefix("api/opinion")]
    public class opinionController : ApiController
    {
        [Route("addOpinion"), HttpPost]
        public IHttpActionResult addOpinion(DTO.OPINION opinion)
        {
            if (BL.opinionBL.addOpinion(opinion))
                return Ok(true);
            else return Ok(false);
        }

            [Route("getOpinionsForGMH"), HttpPost]
        public IHttpActionResult getOpinionsForGMH(DTO.GMH gmh)
        {
            System.Diagnostics.Debug.WriteLine("getOpinionsForGMH");

            return Ok(BL.opinionBL.getOpinionsForGMH(gmh));
        }
    }
}
