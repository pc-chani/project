using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class gmhBL
{
        public static bool updateGMH(GMH gmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return true;
                

            }

        }

        public static GMH[] getMyGmhim(User user)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                return BL.Converters.GMHConverter.convertToDTOarray((db.GMHs.Where(g => g.UserCode == user.UserCode).ToArray()));

            }
        }
    }
}
