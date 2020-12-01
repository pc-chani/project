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
        [Route("getCategories")]
        public IHttpActionResult getCategories()
        {

            return Ok(BL.gmhBL.getCategories());
        }
        [Route("getCategoriesForGmach"), HttpPost]
        public IHttpActionResult getCategoriesForGmach(DTO.CategoryGMH masterGmachCode)
        {

            return Ok(BL.gmhBL.getCategoriesForGmach(masterGmachCode));
        }
        [Route("searchGMH"), HttpPost]
        public IHttpActionResult searchGMH()
        {

            var httpRequest = HttpContext.Current.Request;
            return Ok(BL.gmhBL.searchGMH(
                httpRequest["text"],
                Convert.ToInt32(httpRequest["category"]),
                Convert.ToInt32(httpRequest["tatCategory"]),
                Convert.ToDouble(httpRequest["CurrentLocation1"]),
                Convert.ToDouble(httpRequest["CurrentLocation2"]),
                httpRequest["location"]
                ));
        }
        [Route("getNeedsGmhim")]
        public IHttpActionResult getNeedsGmhim()
        {
            return Ok(BL.needsGmhim.GetNeedsGmhim());
        }
        [Route("filterNeedsGmhim")]
        public IHttpActionResult filterNeedsGmhim()
        {
            var httpRequest = HttpContext.Current.Request;
            return Ok(BL.needsGmhim.filterNeedsGmhim(
                Convert.ToInt32(httpRequest["category"]),
                Convert.ToInt32(httpRequest["tatcategory"]),
                Convert.ToString(httpRequest["adress"])));
        }
        [Route("saveChangesInGmhim")]
        public IHttpActionResult saveChangesInGmhim(DTO.User u)
        {
            return Ok(BL.gmhBL.saveChangesInGmhim(u));
        }
    }

}
