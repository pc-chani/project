using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class needsGmhim
    {
        public static List<NeedsGmhim> GetNeedsGmhim()
        {
            using(DAL.Charity_DBEntities db=new DAL.Charity_DBEntities())
            {
                return BL.Converters.GMHConverter.convertToDTOList(db.NeedsGmhim.ToList());
            }
        }
        public static List<NeedsGmhim> filterNeedsGmhim(int c,int tc,string adress)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                if (c != 0)
                {

                }
                if (tc != 0)
                {

                }
                if(adress!= "undefined")
                {
                    
                }
                return BL.Converters.GMHConverter.convertToDTOList(db.NeedsGmhim.ToList());
            }
        }
    }
}
