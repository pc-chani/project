using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using DTO;

namespace BL
{
    public class reqestBL
    {
      
        public static bool addReqest(RequestForLoan request)
        {
            System.Diagnostics.Debug.WriteLine(request.comment);

            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.RequestForLoan.Add(Converters.RequestForLoanConverter.convertToDal(request));
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

    public class Copy1OfreqestBL
    {

        public static bool addReqest(RequestForLoan request)
        {
            System.Diagnostics.Debug.WriteLine(request.comment);

            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.RequestForLoan.Add(Converters.RequestForLoanConverter.convertToDal(request));
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

    public class CopyOfreqestBL
    {

        public static bool addReqest(RequestForLoan request)
        {
            System.Diagnostics.Debug.WriteLine(request.comment);

            using (DAL.Charity_DBEntities db = new DAL.Charity_DBEntities())
            {
                db.RequestForLoan.Add(Converters.RequestForLoanConverter.convertToDal(request));
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
