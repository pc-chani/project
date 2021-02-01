using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class LendingsBL
    {
        public static bool AddLending(LENDING l)
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
        }//פונקציה שמוסיפה הלוואה לרשימת ההלואות
        public static bool DeleteLending(LENDING l)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                DAL.LENDINGS l1 = db.LENDINGS.SingleOrDefault(le => le.ProductCode == l.ProductCode && le.LendingDate == l.LendingDate);
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
        }//פונקציה שמחוקת הלוואה מרשימת ההלואות
        public static List<LENDING> GetLendings(ProductToGMH p)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                return BL.Converters.LendingConverter.convertToDTOList( db.LENDINGS.Where(l => l.ProductCode == p.ProductCodeToGMH).ToList());
            }
        }//פונקציה שמקבלת מוצר ומחזירה את רשימת ההלואות שלו
        public static bool SetLending(LENDING l)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.LENDINGS.FirstOrDefault(l1 => l1.LendingCode==l.LendingCode).Comment=l.Comment;
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
    }
}
