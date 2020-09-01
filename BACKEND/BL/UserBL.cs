using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
namespace BL
{
    public class UserBL
    {
        public static bool addUser(User user)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.USERS.Add(Converters.UserConverter.convertToDal(user));
                try
                {                  
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("yes");
                    return true;
                }
                catch(DbEntityValidationException ex)
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

        public static bool checkUser(string email, string password)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                if (db.USERS.FirstOrDefault(u => u.E_mail == email && u.Password == password) != null)
                    return true;
                else return false;
                //try
                //{
                //    System.Diagnostics.Debug.WriteLine("yes");
                //}
                //catch (DbEntityValidationException ex)
                //{
                //    //הדפסת שגיאה בקישור לדאטא בס
                //    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                //    {
                //        foreach (var validationError in entityValidationErrors.ValidationErrors)
                //        {
                //            System.Diagnostics.Debug.WriteLine(
                //            "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                //        }
                //    }
                //    System.Diagnostics.Debug.WriteLine("no");
                //}
            }

        }
    }
}
