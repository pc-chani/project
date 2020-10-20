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
        public static DTO.PRODUCT convertToDTO(DAL.Product p)
        {
            return new DTO.PRODUCT
            {
                CategoryCode=p.CategoryCode,
                ProductCode=p.ProductCode,
                Productname=p.Productname,
            };
        }
        public static DAL.Product convertToDal(DTO.PRODUCT p)
        {
            return new DAL.Product
            {
                CategoryCode = p.CategoryCode,
                ProductCode = p.ProductCode,
                Productname = p.Productname,
            };
        }
        public static List<DTO.PRODUCT> convertToDTOList(List<DAL.Product> pList)
        {
            return pList.Select(p => convertToDTO(p)).ToList();
        }

        public static DTO.PRODUCT[] convertToDTOarray(DAL.Product[] pList)
        {
            return pList.Select(p => convertToDTO(p)).ToArray();
        }
    }
}
