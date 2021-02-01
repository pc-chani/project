using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class complaintController : ApiController
    {
        [Route("addComplaint"), HttpPost]
        public IHttpActionResult addComplaint(DTO.Complaint complaint)
        {
            if (BL.complaintBL.addComplaint(complaint))
                return Ok(true);
            else return Ok(false);
        }
    }
}
