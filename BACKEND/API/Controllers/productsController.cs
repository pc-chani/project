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
        [Route ("addProduct"),HttpPost]
        public IHttpActionResult addProduct(DTO.PRODUCT p)
        {

            return Ok(BL.productsBL.addProduct(p));

        }
        [Route("delete"), HttpPost]
        public IHttpActionResult delete(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.delete(p));
        }       
        [Route("postImg"), HttpPost]
        public IHttpActionResult postImg()
        {
            var httpRequest = HttpContext.Current.Request;
            // System.Diagnostics.Debug.WriteLine(httpRequest.Params["product"]);
            var product = new JavaScriptSerializer().DeserializeObject(httpRequest.Params["product"]);
            var dictionary = (Dictionary<string, object>)product;
            DTO.ProductToGMH p=new DTO.ProductToGMH();
            p.ProductCode = (int)dictionary["ProductCode"];
            p.Amount = (int?)dictionary["Amount"];
            p.GmhCode = (int)dictionary["GmhCode"];
            p.FreeDescription = (string)dictionary["FreeDescription"];
            p.IsDisposable = (bool)dictionary["IsDisposable"];
            p.SecurityDepositAmount = (int?)dictionary["SecurityDepositAmount"];
            p.Status = (string)dictionary["Status"];
            string imageName = null;
            //Upload Image
            int c = httpRequest.Files.Count;
            List<string> photos = new List<string>();
            for (int i = 0; i <c; i++)
            {
                var postedFile = httpRequest.Files["Image"+i];
                //Create custom filename
                if (postedFile != null)
                {
                    imageName = postedFile.FileName;
                    //imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                    var filePath = HttpContext.Current.Server.MapPath("~/image/" + imageName);
                    postedFile.SaveAs(filePath);
                    photos.Add(postedFile.FileName);
                }
            }
            return Ok(BL.productsBL.add(p,photos));
        
        }
        [Route("getImg"), HttpPost]
        public IHttpActionResult getImg(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.getImages(p));
        }
        [Route("edit"), HttpPost]
        public IHttpActionResult edit()
        {
            var httpRequest = HttpContext.Current.Request;
            // System.Diagnostics.Debug.WriteLine(httpRequest.Params["product"]);
            var product = new JavaScriptSerializer().DeserializeObject(httpRequest.Params["product"]);
            var dictionary = (Dictionary<string, object>)product;
            DTO.ProductToGMH p = new DTO.ProductToGMH();
            p.ProductCodeToGMH = (int)dictionary["ProductCodeToGMH"];
            p.ProductCode = (int)dictionary["ProductCode"];
            p.FreeDescription = (string)dictionary["FreeDescription"];
            p.IsDisposable = (bool)dictionary["IsDisposable"];
            p.SecurityDepositAmount = (int?)dictionary["SecurityDepositAmount"];
            p.Status = (string)dictionary["Status"] ;
            string imageName = null;
            //Upload Image
            int c = httpRequest.Files.Count;
            List<string> photos = new List<string>();
            for (int i = 0; i < c; i++)
            {
                var postedFile = httpRequest.Files["Image" + i];
                //Create custom filename
                if (postedFile != null)
                {
                    imageName = postedFile.FileName;
                   // imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                    var filePath = HttpContext.Current.Server.MapPath("~/image/" + imageName);
                    postedFile.SaveAs(filePath);
                    photos.Add(postedFile.FileName);
                }
            }
            return Ok(BL.productsBL.edit(p,photos));
        }
    }
}
