using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class gmhBL
{
        public static bool addGMH(GMH gmh)
        {
            
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.GMHs.Add(Converters.GMHConverter.convertToDal(gmh));
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
        public static bool delete(GMH gmh)
        {

            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                //List<ProductToGMH> pr =BL.Converters.ProductToGmhConverter.convertToDTOList(db.PRODUCTtoGMHs.Where(p => p.GmhCode == gmh.GmhCode).ToList());
                //pr.ForEach(p => db.Products.Remove(db.Products.FirstOrDefault(p1=>p1.ProductCode== p.ProductCode)));
                db.PRODUCTtoGMHs.RemoveRange(db.PRODUCTtoGMHs.Where(p => p.GmhCode == gmh.GmhCode));
                db.GMHs.Remove((db.GMHs.SingleOrDefault(g => g.GmhCode == gmh.GmhCode)));
                try
                {
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("yes");
                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    //הדפסת שגיאה בקישור לדאטא בס
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
        public static GMH[] getMyGmhim(User user)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                
                    return BL.Converters.GMHConverter.convertToDTOarray((db.GMHs.Where(g => g.UserCode == user.UserCode).ToArray()));
                
            }
        }
        public static bool saveChange(GMH gmh)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {


                try
                {
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("yes");
                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    //הדפסת שגיאה בקישור לדאטא בס
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
