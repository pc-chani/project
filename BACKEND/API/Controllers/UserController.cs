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
        [Route("GetUser"),HttpPost]//פונקציה שממקבלת גמ"ח ומחזירה את המנהל שלה
        public IHttpActionResult GetUser(DTO.GMH gMH)
        {
            return Ok(BL.UserBL.GetUser(gMH));
        }
       // [Route("getmyuser"),HttpPost]//פונקציה שמקבלת קוד משתמש ומחזירה את הנתונים שלו
       // public IHttpActionResult get([FromBody] int code)
       // {
       //     return Ok(BL.UserBL.getUser(code));
       // }
        [Route("CheckUser"), HttpPost]//פונקציה שבודקת האם המשתמש קיים במערכת
        public IHttpActionResult CheckUser(DTO.User user)
        {
          return Ok(BL.UserBL.CheckUser(user.E_mail, user.Password));           
        }
        [Route("AddUser"),HttpPost]//פונקציה שמוספה משתמש
        public IHttpActionResult AddUser(DTO.User user)
        {
            return Ok (BL.UserBL.AddUser(user));
        }
        [Route("SaveChanges"), HttpPost]//פונקציה ששומרת שינויים לפרטי משתמש
        public IHttpActionResult SaveChanges(DTO.User user)
        {
            return Ok(BL.UserBL.SaveChanges(user));
        }
        

    }
}
