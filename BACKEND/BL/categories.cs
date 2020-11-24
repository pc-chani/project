using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class categories
    {
        public static List<DTO.CategoryGMH> GetCategories()
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {

                return BL.Converters.CategoryGMHConvereter.convertToDTOList((db.CategoryGMH.Select(c=>c).ToList()));

            }
        }

        public static int addCategory(CategoryGMH c)
        {
            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
               if(c.MasterCategoryCode==0) c.MasterCategoryCode = 5;
                
                db.CategoryGMH.Add( BL.Converters.CategoryGMHConvereter.convertToDAL(c));
                
                try
                {
                    db.SaveChanges();
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
                }
                db.CategoryGMH.ToArray().Last().MasterCategoryCode = null;
                return db.CategoryGMH.ToArray().Last().CategoryCode;
            }

        }
    }
}
