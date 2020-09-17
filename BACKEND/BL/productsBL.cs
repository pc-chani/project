using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class productsBL
    {
        public static ProductToGMH[] getProducts(GMH gmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.ProductToGmhConverter.convertToDTOarray(db.PRODUCTtoGMHs.Where(p=>p.GmhCode==gmh.GmhCode).ToArray());
            }

        }

        public static PRODUCT getProduct(ProductToGMH pTgmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.ProductConverter.convertToDTO(db.Products.FirstOrDefault(p => p.ProductCode == pTgmh.ProductCode));
            }
        }
        
    }
}
