using Domain;
using Domain.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApplication
{
    public interface IUserCrud
    {
        public int AddUser(UserDTO user);
        public void DeleteUser(int userID);
        public User GetUser(int userID);
        public void UpdateUser(User user);
    }
}
