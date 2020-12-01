using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{

    [EnableCors("*","*","*")]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [Route("getUser"),HttpPost]
        [HttpGet]//brirat mecdak
        public IHttpActionResult getUser(DTO.GMH gMH)
        {
            return Ok(BL.UserBL.getUser(gMH));
        }
        [Route("getmyuser"),HttpPost]
        public IHttpActionResult get([FromBody] int code)
        {
            return Ok(BL.UserBL.getUser(code));
        }
        [Route("checkUser"), HttpPost]
        public IHttpActionResult checkUser(DTO.User user)
        {
          return Ok(BL.UserBL.checkUser(user.E_mail, user.Password));           
        }
        [Route("addUser"),HttpPost]
        public IHttpActionResult addUser(DTO.User user)
        {
            return Ok (BL.UserBL.addUser(user));
        }
        [Route("saveChanges"), HttpPost]
        public IHttpActionResult saveChanges(DTO.User user)
        {
            return Ok(BL.UserBL.saveChanges(user));
        }
        

    }
}
