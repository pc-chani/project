using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class LendingConverter
    {
        public static DTO.LENDING convertToDTO(DAL.LENDINGS l)
        {
            return new DTO.LENDING
            {
            Comment=l.Comment,
            LendingCode=l.LendingCode,
            LendingDate=l.LendingDate,
            ProductCode=l.ProductCode,
            ReturnDate=l.ReturnDate,
            UserCode=l.UserCode
            };
        }
        public static DAL.LENDINGS convertToDAL(DTO.LENDING l)
        {
            return new DAL.LENDINGS
            {
                Comment = l.Comment,
                LendingCode = l.LendingCode,
                LendingDate = l.LendingDate,
                ProductCode = l.ProductCode,
                ReturnDate = l.ReturnDate,
                UserCode = l.UserCode
            };
        }
        public static List<DTO.LENDING> convertToDTOList(List<DAL.LENDINGS> lList)
        {
            return lList.Select(l => convertToDTO(l)).ToList();
        }
    }
}
