using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class UserConverter
    {
        public static DTO.User convertToDTO(DAL.USER u)
        {
            return new DTO.User
            {
               UserCode=u.UserCode,
               Adress=u.Adress,
               Cell_Phone=u.Cell_Phone,
               E_mail=u.E_mail,
               FirstName=u.FirstName,
               LastName=u.LastName,
               Password=u.Password,
               Permission=u.Permission,
               Phone=u.Phone
            };
        }
        public static DAL.USER convertToDal(DTO.User u)
        {
            return new DAL.USER
            {
                UserCode = u.UserCode,
                Adress = u.Adress,
                Cell_Phone = u.Cell_Phone,
                E_mail = u.E_mail,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Password = u.Password,
                Permission = u.Permission,
                Phone = u.Phone
            };
        }
        public static List<DTO.User> convertToDTOList(List<DAL.USER> uList)
        {
            return uList.Select(u => convertToDTO(u)).ToList();
        }
    }
}
