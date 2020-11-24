using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
   public class ImagesConverter
    {
        public static DTO.Images convertToDTO(DAL.Images i)
        {
            return new DTO.Images
            {
               ProductCodeToGMH=i.ProductCodeToGMH,
               Path=i.Path,
               ImageCode=i.ImageCode

            };
        }
        public static DAL.Images convertToDAL(DTO.Images i)
        {
            return new DAL.Images
            {
                ProductCodeToGMH = i.ProductCodeToGMH,
                Path = i.Path,
                ImageCode = i.ImageCode

            };
        }
        public static List<DTO.Images> convertToDTOList(List<DAL.Images> iList)
        {
            return iList.Select(i => convertToDTO(i)).ToList();
        }
    }
}
