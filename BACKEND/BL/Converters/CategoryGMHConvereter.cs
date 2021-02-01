using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class CategoryGMHConvereter
    {
        public static DTO.CategoryGMH convertToDTO(DAL.CategoryGMH c)
        {
            return new DTO.CategoryGMH
            {
                CategoryCode = c.CategoryCode,
                CategoryName = c.CategoryName,
                MasterCategoryCode = Convert.ToInt32( c.MasterCategoryCode)

            };
        }
        public static DAL.CategoryGMH convertToDAL(DTO.CategoryGMH c)
        {
            return new DAL.CategoryGMH
            {
                CategoryCode = c.CategoryCode,
                CategoryName = c.CategoryName,
                MasterCategoryCode = c.MasterCategoryCode

            };
        }
        public static List<DTO.CategoryGMH> convertToDTOList(List<DAL.CategoryGMH> cList)
        {
            return cList.Select(c => convertToDTO(c)).ToList();
        }
    }
}
