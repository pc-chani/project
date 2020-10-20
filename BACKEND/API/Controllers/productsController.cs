using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{

    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/products")]
    public class productsController : ApiController
    {
        [Route("getProducts")]
        public IHttpActionResult getProducts()
        {
            return Ok(BL.productsBL.getProducts());
        }

        [Route("getProductsAccordingToGmhCategory"), HttpPost]
        public IHttpActionResult getProductsAccordingToGmhCategory(DTO.GMH gMH)
        {
            return Ok(BL.productsBL.getProductsAccordingToGmhCategory(gMH));
        }
        

        [Route("getProductsForGMH"), HttpPost]
        public IHttpActionResult getProductsForGMH(DTO.GMH gMH)
        {
                return Ok(BL.productsBL.getProductsForGMH(gMH));
        }
        [Route("getProduct"), HttpPost]
        public IHttpActionResult getProduct(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.getProduct(p));
        }
        [Route("saveChange"), HttpPost]
        public IHttpActionResult saveChange(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.saveChange(p));
        }
        [Route("add"), HttpPost]
        public IHttpActionResult add(DTO.ProductToGMH p)
        {
            System.Diagnostics.Debug.WriteLine(p.Picture);
            return Ok(BL.productsBL.add(p));
        }
        [Route("delete"), HttpPost]
        public IHttpActionResult delete(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.delete(p));
        }
        //trying func
        [Route("postImg"), HttpPost]
        public IHttpActionResult postImg()
        {
            string imageName = null;
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            var postedFile = httpRequest.Files["Image"];
            //Create custom filename
            if (postedFile != null)
            {
                imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/image/" + imageName);
                postedFile.SaveAs(filePath);

            }
            return Ok();
        }
    }
}
