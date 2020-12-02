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
    public class donationsController : ApiController
    {
        [Route("getDonations")]
        public IHttpActionResult getDonations()
        {          
          return Ok(BL.donationsBL.GetDonations());
        }
        [Route("getDonation"),HttpPost]
        public IHttpActionResult getDonation([FromBody] int code)
        {
            return Ok(BL.donationsBL.GetDonation(code));
        }
        [Route("AddDonation")]
        public IHttpActionResult AddDonation()
        {
            var httpRequest = HttpContext.Current.Request;
            // System.Diagnostics.Debug.WriteLine(httpRequest.Params["product"]);
            var donation = new JavaScriptSerializer().DeserializeObject(httpRequest.Params["donation"]);
            var dictionary = (Dictionary<string, object>)donation;
            DTO.Donations d = new DTO.Donations();
            d.Adress = (string)dictionary["Adress"];
            d.MasterCategory = (int?)dictionary["MasterCategory"];
            d.Category = (int?)dictionary["Category"];

            d.Description = (string)dictionary["Description"];

            d.Phone = (string)dictionary["Phone"];
            d. donationName= (string)dictionary["donationName"];

            d.donorName = (string)dictionary["donorName"];
            d.donorEmail= (string)dictionary["donorEmail"];

            string imageName = null;
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
            return Ok(BL.donationsBL.AddDonations(d));
        }
        [Route("DeleteDonation")]
        public IHttpActionResult DeleteDonation(DTO.Donations d)
        {
            return Ok(BL.donationsBL.RemoveDonation(d));
        }
        [Route("filterDonations")]
        public IHttpActionResult filterDonations()
        {
            var httpRequest = HttpContext.Current.Request;
            return Ok(BL.donationsBL.filterDonations(
                Convert.ToInt32(httpRequest["category"]),
                Convert.ToInt32(httpRequest["tatcategory"]),
                Convert.ToString(httpRequest["adress"])));
        }
        [Route("saveChanges")]
        public IHttpActionResult saveChanges()
        {
            var httpRequest = HttpContext.Current.Request;
            // System.Diagnostics.Debug.WriteLine(httpRequest.Params["product"]);
            var donation = new JavaScriptSerializer().DeserializeObject(httpRequest.Params["donation"]);
            var dictionary = (Dictionary<string, object>)donation;
            DTO.Donations d = new DTO.Donations();
            d.donationCode = (int)dictionary["donationCode"];
            d.Description = (string)dictionary["Description"];
            d.donationName = (string)dictionary["donationName"];
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
                d.Picture = postedFile.FileName;
            }
            return Ok(BL.donationsBL.saveChanges(d));
        }

    }
}
