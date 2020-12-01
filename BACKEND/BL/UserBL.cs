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
        public static bool saveChanges(User user)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.USERS.FirstOrDefault(u => u.UserCode == user.UserCode).Name = user.Name;
               // db.USERS.FirstOrDefault(u => u.UserCode == user.UserCode).Password = user.Password;
                db.USERS.FirstOrDefault(u => u.UserCode == user.UserCode).Phone = user.Phone;
                db.USERS.FirstOrDefault(u => u.UserCode == user.UserCode).E_mail = user.E_mail;
               // db.USERS.FirstOrDefault(u => u.UserCode == user.UserCode).Cell_Phone = user.Cell_Phone;
                db.USERS.FirstOrDefault(u => u.UserCode == user.UserCode).Adress = user.Adress;
                try { db.SaveChanges(); return true; }
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
        public static User checkUser(string email, string password)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {   if (db.USERS.FirstOrDefault(u => u.E_mail == email && u.Password == password) != null)
                {
                    User user = BL.Converters.UserConverter.convertToDTO(db.USERS.FirstOrDefault(u => u.E_mail == email && u.Password == password));
                    return user;
                }
                else return null;

            }

        }
        public static User getUser(GMH gMH)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                if(gMH!=null)
                return BL.Converters.UserConverter.convertToDTO(db.USERS.FirstOrDefault(u=>u.UserCode==gMH.UserCode));
                return new User();
            }

        }
        public static User getUser(int code)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
             return BL.Converters.UserConverter.convertToDTO(db.USERS.FirstOrDefault(u => u.UserCode == code));
             
            }

        }
    }
}
