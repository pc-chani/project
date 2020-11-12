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
        [Route("AddDonation")]
        public IHttpActionResult AddDonation()
        {
            var httpRequest = HttpContext.Current.Request;
            // System.Diagnostics.Debug.WriteLine(httpRequest.Params["product"]);
            var donation = new JavaScriptSerializer().DeserializeObject(httpRequest.Params["donation"]);
            var dictionary = (Dictionary<string, object>)donation;
            DTO.Donations d = new DTO.Donations();
            d.Adress = (string)dictionary.ElementAt(0).Value;
            d.MasterCategory = (int?)dictionary.ElementAt(1).Value;
            d.Description = (string)dictionary.ElementAt(2).Value;

            d.Phone = (string)dictionary.ElementAt(3).Value;
            d. donationName= (string)dictionary.ElementAt(4).Value;
            d.donorName = (string)dictionary.ElementAt(5).Value;
         //   d.Phone = (string)dictionary.ElementAt(6).Value;
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
                    d.Picture=postedFile.FileName;
                }
            return Ok(BL.donationsBL.AddDonations(d));
        }
        [Route("DeleteDonation")]
        public IHttpActionResult DeleteDonation(DTO.Donations d)
        {
            return Ok(BL.donationsBL.RemoveDonation(d));
        }
    }
}
