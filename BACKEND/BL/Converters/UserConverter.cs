using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Converters
{
    public class UserConverter
    {
        public static DTO.User convertToDTO(DAL.USERS u)
        {
            return new DTO.User
            {
               UserCode=u.UserCode,
               Adress=u.Adress,
               Cell_Phone=u.Cell_Phone,
               E_mail=u.E_mail,
               Name=u.Name,
               Password=u.Password,
               Permission=u.Permission,
               Phone=u.Phone
            };
        }
        public static DAL.USERS convertToDal(DTO.User u)
        {
            return new DAL.USERS
            {
                UserCode = u.UserCode,
                Adress = u.Adress,
                Cell_Phone = u.Cell_Phone,
                E_mail = u.E_mail,
                Name=u.Name,
                Password = u.Password,
                Permission = u.Permission,
                Phone = u.Phone
            };
        }
        public static List<DTO.User> convertToDTOList(List<DAL.USERS> uList)
        {
            return uList.Select(u => convertToDTO(u)).ToList();
        }
    }
}
