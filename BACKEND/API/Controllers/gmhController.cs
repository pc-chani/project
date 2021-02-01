using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/gmh")]
    public class GmhController : ApiController
    {
        [Route("Add"), HttpPost]//פונקציה שמקבלת גמ"ח ומוסיפה אותו לטבלת הגמח"ים
        public IHttpActionResult Add(DTO.GMH gMH)
        {

            return Ok(BL.GmhBL.AddGMH(gMH));
   
        }
        [Route("GetMyGmhim"), HttpPost]//פונקציה שמחזירה את רשימת הגמח"ים
        public IHttpActionResult GetMyGmhim(DTO.User user)
        {
            return Ok(BL.GmhBL.GetMyGmhim(user));      
        }
        [Route("Delete"), HttpPost]//פונקציה שמקבלת גמ"ח ומוחקת אותו מרשימת הגמח"ים
        public IHttpActionResult Delete(DTO.GMH gMH)
        {
            return Ok(BL.GmhBL.Delete(gMH));
        }
        [Route("SaveChange"), HttpPost]//פונקציה שמקבלת גמ"ח ושומרת בו את השינויים
        public IHttpActionResult SaveChange(DTO.GMH gMH)
        {
            return Ok(BL.GmhBL.SaveChange(gMH));
        }
        [Route("GetCategories")]
        public IHttpActionResult GetCategories()
        {

            return Ok(BL.GmhBL.GetCategories());
        }
        [Route("GetCategoriesForGmach"), HttpPost]
        public IHttpActionResult GetCategoriesForGmach(DTO.CategoryGMH masterGmachCode)
        {

            return Ok(BL.GmhBL.GetCategoriesForGmach(masterGmachCode));
        }
        [Route("SearchGMH"), HttpPost]
        public IHttpActionResult SearchGMH()
        {

            var httpRequest = HttpContext.Current.Request;
            return Ok(BL.GmhBL.SearchGMH(
                httpRequest["text"],
                Convert.ToInt32(httpRequest["category"]),
                Convert.ToInt32(httpRequest["tatCategory"]),
                Convert.ToDouble(httpRequest["CurrentLocation1"]),
                Convert.ToDouble(httpRequest["CurrentLocation2"]),
                httpRequest["location"]
                ));
        }
        [Route("GetNeedsGmhim")]
        public IHttpActionResult GetNeedsGmhim()
        {
            return Ok(BL.NeedsGMHim.GetNeedsGmhim());
        }
        [Route("FilterNeedsGmhim")]
        public IHttpActionResult FilterNeedsGmhim()
        {
            var httpRequest = HttpContext.Current.Request;
            return Ok(BL.NeedsGMHim.FilterNeedsGmhim(
                Convert.ToInt32(httpRequest["category"]),
                Convert.ToInt32(httpRequest["tatcategory"]),
                Convert.ToString(httpRequest["adress"])));
        }
        [Route("SaveChangesInGmhim")]
        public IHttpActionResult SaveChangesInGmhim(DTO.User u)
        {
            return Ok(BL.GmhBL.SaveChangesInGmhim(u));
        }
    }

}
