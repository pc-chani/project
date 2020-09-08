using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class ProductToGmhConverter
    {
        public static DTO.ProductToGMH convertToDTO(DAL.PRODUCTtoGMH p)
        {
            return new DTO.ProductToGMH
            {
               Amount=p.Amount,
               FreeDescription=p.FreeDescription,
               GmhCode=p.GmhCode,
               IsDisposable=p.IsDisposable,
              Picture=p.Picture,
              ProductCode=p.ProductCode,
              ProductCodeToGMH=p.ProductCodeToGMH,
              SecurityDepositAmount=p.SecurityDepositAmount,
              Status=p.Status
         
            };
        }
        public static DAL.PRODUCTtoGMH convertToDal(DTO.ProductToGMH p)
        {
            return new DAL.PRODUCTtoGMH
            {

                Amount = p.Amount,
                FreeDescription = p.FreeDescription,
                GmhCode = p.GmhCode,
                IsDisposable = p.IsDisposable,
                Picture = p.Picture,
                ProductCode = p.ProductCode,
                ProductCodeToGMH = p.ProductCodeToGMH,
                SecurityDepositAmount = p.SecurityDepositAmount,
                Status = p.Status

            };
        }
        public static List<DTO.ProductToGMH> convertToDTOList(List<DAL.PRODUCTtoGMH> pList)
        {
            return pList.Select(p => convertToDTO(p)).ToList();
        }
        public static DTO.ProductToGMH[] convertToDTOarray(DAL.PRODUCTtoGMH[] pList)
        {
            return pList.Select(p => convertToDTO(p)).ToArray();
        }
    }
}
