using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        public static bool saveChange(ProductToGMH pTgmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return true;
            }
        }
        public static bool add(ProductToGMH pTgmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.PRODUCTtoGMHs.Add(Converters.ProductToGmhConverter.convertToDal( pTgmh));
                try
                {
                    db.SaveChanges();
                    return true;
                }
                     catch (DbEntityValidationException ex) {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine(
                            "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("no");
                    return false; }
            }
        }
        public static bool remove(ProductToGMH pTgmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return true;
            }
        }
    }
}
