using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BL.Converters
{
   public class ProductConverter
    {
        public static DTO.PRODUCT convertToDTO(DAL.Products p)
        {
            return new DTO.PRODUCT
            {
                CategoryCode=p.CategoryCode,
                ProductCode=p.ProductCode,
                Productname=p.Productname,
            };
        }
        public static DAL.Products convertToDal(DTO.PRODUCT p)
        {
            return new DAL.Products
            {
                CategoryCode = p.CategoryCode,
                ProductCode = p.ProductCode,
                Productname = p.Productname,
            };
        }
        public static List<DTO.PRODUCT> convertToDTOList(List<DAL.Products> pList)
        {
            return pList.Select(p => convertToDTO(p)).ToList();
        }

        public static DTO.PRODUCT[] convertToDTOarray(DAL.Products[] pList)
        {
            return pList.Select(p => convertToDTO(p)).ToArray();
        }
    }
}
