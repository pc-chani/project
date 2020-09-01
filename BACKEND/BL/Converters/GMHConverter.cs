using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class GMHConverter
    {
        public static DTO.GMH convertToDTO(DAL.GMH g)
        {
            return new DTO.GMH
            {
                GmeName=g.GmeName,
                GmhCode=g.GmhCode,
                Adress=g.Adress,
                Amount=g.Amount,
                CategoryCode=g.CategoryCode,
                FreeDescription=g.FreeDescription,
                IsDisposable=g.IsDisposable,
                Picture=g.Picture,
                SecurityDepositAmount=g.SecurityDepositAmount,
                Status=g.Status,
                UserCode=g.UserCode
            };
        }
        public static DAL.GMH convertToDal(DTO.GMH g)
        {
            return new DAL.GMH
            {
                GmeName = g.GmeName,
                GmhCode = g.GmhCode,
                Adress = g.Adress,
                Amount = g.Amount,
                CategoryCode = g.CategoryCode,
                FreeDescription = g.FreeDescription,
                IsDisposable = g.IsDisposable,
                Picture = g.Picture,
                SecurityDepositAmount = g.SecurityDepositAmount,
                Status = g.Status,
                UserCode = g.UserCode

            };
        }
        public static List<DTO.GMH> convertToDTOList(List<DAL.GMH> gList)
        {
            return gList.Select(g => convertToDTO(g)).ToList();
        }
    }
}
