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
    public class LendingsController : ApiController
    {
        [Route("AddLending"), HttpPost]//פונקציה שמוסיפה הלוואה לרשימת ההלואות
        public IHttpActionResult AddLending(DTO.LENDING l)
        {          
            return Ok(BL.LendingsBL.AddLending(l));
        }
        [Route("GetLendings"), HttpPost]//פונקציה שמקבלת מוצר ומחזירה את רשימת ההלואות שלו
        public IHttpActionResult GetLendings(DTO.ProductToGMH p)
        {
            return Ok(BL.LendingsBL.GetLendings(p));
        }
        [Route("DeleteLending"), HttpPost]//פונקציה שמחוקת הלוואה מרשימת ההלואות
        public IHttpActionResult DeleteLending(DTO.LENDING l)
        {
            return Ok(BL.LendingsBL.DeleteLending(l));
        }
    }
}
