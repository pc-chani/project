using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BL
{
   public class GoogleMaps
    {
        public static int GetDistance(string origin, string destination)
        {

            string uri = "https://maps.googleapis.com/maps/api/distancematrix/xml?key=AIzaSyA5L81_-5d2Hy7hHsNVhodk1zS90Qu-aP8&origins="
                          + origin + "&destinations=" + destination + "&mode=driving&units=imperial&sensor=false";
            WebClient wc = new WebClient();
            try
            {
                string geoCodeInfo = wc.DownloadString(uri);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(geoCodeInfo);
               // if (xmlDoc.DocumentElement.SelectSingleNode("/DistanceMatrixResponse/row/element/duration/value") != null)
                //{
                    string duration = xmlDoc.DocumentElement.SelectSingleNode("/DistanceMatrixResponse/row/element/duration/value").InnerText;
                    return Convert.ToInt32(duration) / 60;
               // }
               // return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
