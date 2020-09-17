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
        [Route("getuser"),HttpPost]
        [HttpGet]//brirat mecdak
        public IHttpActionResult getUser(DTO.GMH gMH)
        {
            return Ok(BL.UserBL.getUser(gMH));
        }
        [Route("get1")]
        public int Get1()
        {
            return 1;
        }
        [Route("checkUser"), HttpPost]
        public IHttpActionResult checkUser(DTO.User user)
        {
          return Ok(BL.UserBL.checkUser(user.E_mail, user.Password));           
        }
        [Route("addUser"),HttpPost]
        public IHttpActionResult addUser(DTO.User user)
        {
            if (BL.UserBL.addUser(user))
                return Ok(true);
            else return Ok(false);
        }
     
    }
}
