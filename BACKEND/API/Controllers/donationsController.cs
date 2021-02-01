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
    [RoutePrefix("api/donation")]
    public class DonationsController : ApiController
    {
        [Route("GetDonations")]//פונקציה שמחזירה את כל התרומות
        public IHttpActionResult GetDonations()
        {          
          return Ok(BL.DonationsBL.GetDonations());
        }
        [Route("GetDonation"),HttpPost]// פונקציה שמקבלת קוד תרומה ומחזירה את אותה תרומה 
        public IHttpActionResult GetDonation([FromBody] int code)
        {
            return Ok(BL.DonationsBL.GetDonation(code));
        }
        [Route("AddDonation")]//פונקציה שמקבלת תרומה ומוסיפה אותה לטבלת התרומות
        public IHttpActionResult AddDonation()
        {
            var httpRequest = HttpContext.Current.Request;
            // System.Diagnostics.Debug.WriteLine(httpRequest.Params["product"]);
            var donation = new JavaScriptSerializer().DeserializeObject(httpRequest.Params["donation"]);
            var dictionary = (Dictionary<string, object>)donation;
            DTO.Donations d = new DTO.Donations { };
            d.Adress = (string)dictionary["Adress"];
          //  d.MasterCategory = (int?)dictionary["MasterCategory"];
            d.Category = (int)dictionary["Category"];

            d.Description = (string)dictionary["Description"];

            d.Phone = (string)dictionary["Phone"];
            d. donationName= (string)dictionary["donationName"];

            d.donorName = (string)dictionary["donorName"];
            d.donorEmail= (string)dictionary["donorEmail"];

          //  string imageName = null;
            //Upload Image          
                var postedFile = httpRequest.Files["Image"];
                //Create custom filename
                if (postedFile != null)
                {
                    //imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                    //imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                    var filePath = HttpContext.Current.Server.MapPath("~/image/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    d.Picture=postedFile.FileName;
                }
            return Ok(BL.DonationsBL.AddDonations(d));
        }
        [Route("DeleteDonation")]//פונקציה שמקבלת תרומה ומוחקת אותה מטבלת בתרומות
        public IHttpActionResult DeleteDonation(DTO.Donations d)
        {
            return Ok(BL.DonationsBL.RemoveDonation(d));
        }
        [Route("FilterDonations")]//פונקציה שמקבלת קריטריונים ומחזירה את התרומות המתאימות להם
        public IHttpActionResult FilterDonations()
        {
            var httpRequest = HttpContext.Current.Request;
            return Ok(BL.DonationsBL.FilterDonations(
                Convert.ToInt32(httpRequest["category"]),
                Convert.ToInt32(httpRequest["tatcategory"]),
                Convert.ToString(httpRequest["adress"])));
        }
        [Route("SaveChanges")]
        public IHttpActionResult SaveChanges()
        {
            var httpRequest = HttpContext.Current.Request;
            // System.Diagnostics.Debug.WriteLine(httpRequest.Params["product"]);
            var donation = new JavaScriptSerializer().DeserializeObject(httpRequest.Params["donation"]);
            var dictionary = (Dictionary<string, object>)donation;
            DTO.Donations d = new DTO.Donations { };
            d.donationCode = (int)dictionary["donationCode"];
            d.Description = (string)dictionary["Description"];
            d.donationName = (string)dictionary["donationName"];
            string imageName;
            //Upload Image          
            var postedFile = httpRequest.Files["Image"];
            //Create custom filename
            if (postedFile != null)
            {
                imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/image/" + imageName);
                postedFile.SaveAs(filePath);
                d.Picture = postedFile.FileName;
            }
            return Ok(BL.DonationsBL.SaveChanges(d));
        }

    }
}
