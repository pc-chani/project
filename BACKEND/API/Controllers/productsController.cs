using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;

namespace API.Controllers
{

    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/products")]
    public class productsController : ApiController
    {
        [Route("getProducts"), HttpPost]
        public IHttpActionResult getProducts(DTO.GMH gMH)
        {
                return Ok(BL.productsBL.getProducts(gMH));
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
         //   System.Diagnostics.Debug.WriteLine(p.Picture);
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
            var httpRequest = HttpContext.Current.Request;
            var product = new JavaScriptSerializer().Deserialize<DTO.ProductToGMH>(httpRequest["product"]);
           

            System.Diagnostics.Debug.WriteLine(product);
            string imageName = null;
           
            //Upload Image
            var postedFile = httpRequest.Files["Image"];
            //Create custom filename
            if (postedFile != null)
            {
                imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/image/" + imageName);
                postedFile.SaveAs(filePath);
                   product.Picture = filePath;
               return Ok(BL.productsBL.add(product));
            }
            return Ok(false);
        }
       
    }
}
