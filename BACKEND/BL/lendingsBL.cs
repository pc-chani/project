using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class lendingsBL
    {
        public static bool addLending(LENDING l)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.LENDINGS.Add(Converters.LendingConverter.convertToDAL(l));
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine(
                            "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("no");
                    return false;
                }
            }
        }

        public static bool deleteLending(LENDING l)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                DAL.LENDING l1 = db.LENDINGS.SingleOrDefault(le => le.ProductCode == l.ProductCode);
                db.LENDINGS.Remove(l1);
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {

                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine(
                            "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("no");
                    return false;
                }
            }
        }

        public static List<LENDING> getLendings(ProductToGMH p)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.LendingConverter.convertToDTOList( db.LENDINGS.Where(l => l.ProductCode == p.ProductCodeToGMH).ToList());
            }
        }
    }
}
