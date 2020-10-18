using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class categories
    {
        public static List<DTO.CategoryGMH> GetCategories()
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                return BL.Converters.CategoryGMHConvereter.convertToDTOList((db.CategoryGMHs.Select(c=>c).ToList()));

            }
        }
    }
}
