using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfrastructure
{
    public interface IUserRepository
    {
        public int AddUser(User user);
        public void DeleteUser(int userID);
        public void UpdateUser(User user);
        public User GetUser(int userID);
    }
}
