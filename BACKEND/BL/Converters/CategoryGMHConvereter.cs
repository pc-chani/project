using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class CategoryGMHConvereter
    {
        public static DTO.CategoryGMH ConvertToDTO(DAL.CategoryGMH c)
        {
            return new DTO.CategoryGMH
            {
                CategoryCode = c.CategoryCode,
                CategoryName = c.CategoryName,
                MasterCategoryCode = Convert.ToInt32( c.MasterCategoryCode)

            };
        }
        public static DAL.CategoryGMH ConvertToDAL(DTO.CategoryGMH c)
        {
            return new DAL.CategoryGMH
            {
                CategoryCode = c.CategoryCode,
                CategoryName = c.CategoryName,
                MasterCategoryCode = c.MasterCategoryCode

            };
        }
        public static List<DTO.CategoryGMH> ConvertToDTOList(List<DAL.CategoryGMH> cList)
        {
            return cList.Select(c => ConvertToDTO(c)).ToList();
        }
    }
}
