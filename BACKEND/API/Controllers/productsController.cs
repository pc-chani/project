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
    public class ProductsController : ApiController
    {
        [Route("GetProducts")]//פונקציה שמחזירה את רשימת המוצרים
        public IHttpActionResult GetProducts()
        {
            return Ok(BL.productsBL.getProducts());
        }
        [Route("GetProductsAccordingToGmhCategory"), HttpPost]//פונקציה שמחזירה מוצרים לפי קטגוריה
        public IHttpActionResult GetProductsAccordingToGmhCategory(DTO.GMH gMH)
        {
            return Ok(BL.productsBL.getProductsAccordingToGmhCategory(gMH));
        }     
        [Route("GetProductsForGMH"), HttpPost]//פונקציה שמחזירה מוצרים של גמ"ח
        public IHttpActionResult GetProductsForGMH(DTO.GMH gMH)
        {
                return Ok(BL.productsBL.getProductsForGMH(gMH));
        }
        [Route("GetProduct"), HttpPost]//פונקציה שמחזירה מוצר לפי קוד
        public IHttpActionResult GetProduct(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.getProduct(p));
        }
        [Route("SaveChange"), HttpPost]//פונקציה ששומרת שינויים של מוצר
        public IHttpActionResult SaveChange(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.saveChange(p));
        }   
        [Route ("AddProduct"),HttpPost]//פונקציה שמוסיפה מוצר
        public IHttpActionResult AddProduct(DTO.PRODUCT p)
        {

            return Ok(BL.productsBL.addProduct(p));

        }
        [Route("Delete"), HttpPost]//פונקציה שמוחקת מוצר
        public IHttpActionResult Delete(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.delete(p));
        }       
        [Route("PostImg"), HttpPost]//פונקציה ששומרת תמונה של מוצר 
        public IHttpActionResult PostImg()
        {
            var httpRequest = HttpContext.Current.Request;
            // System.Diagnostics.Debug.WriteLine(httpRequest.Params["product"]);
            var product = new JavaScriptSerializer().DeserializeObject(httpRequest.Params["product"]);
            var dictionary = (Dictionary<string, object>)product;
            DTO.ProductToGMH p=new DTO.ProductToGMH { };
            p.ProductCode = (int)dictionary["ProductCode"];
            p.Amount = (int?)dictionary["Amount"];
            p.GmhCode = (int)dictionary["GmhCode"];
            p.FreeDescription = (string)dictionary["FreeDescription"];
            p.IsDisposable = (bool)dictionary["IsDisposable"];
            p.SecurityDepositAmount = (int?)dictionary["SecurityDepositAmount"];
            p.Status = (string)dictionary["Status"];
            string imageName;
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
        [Route("GetImg"), HttpPost]//פונקציה שמחזירה תמונה של מוצר
        public IHttpActionResult GetImg(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.getImages(p));
        }
        [Route("Edit"), HttpPost]//פונקציה שעורכת פרטי מוצר 
        public IHttpActionResult Edit()
        {
            var httpRequest = HttpContext.Current.Request;
            // System.Diagnostics.Debug.WriteLine(httpRequest.Params["product"]);
            var product = new JavaScriptSerializer().DeserializeObject(httpRequest.Params["product"]);
            var dictionary = (Dictionary<string, object>)product;
            DTO.ProductToGMH p = new DTO.ProductToGMH { } ;
            p.ProductCodeToGMH = (int)dictionary["ProductCodeToGMH"];
            p.ProductCode = (int)dictionary["ProductCode"];
            p.FreeDescription = (string)dictionary["FreeDescription"];
            p.IsDisposable = (bool)dictionary["IsDisposable"];
            p.SecurityDepositAmount = (int?)dictionary["SecurityDepositAmount"];
            p.Status = (string)dictionary["Status"] ;
            string imageName;
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
