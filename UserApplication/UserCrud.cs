using Domain;
using Domain.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfrastructure;

namespace UserApplication
{
    public class UserCrud : IUserCrud
    {
        private readonly IUserRepository _userRepository;
        public UserCrud(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public int AddUser(UserDTO user)
        {
            return _userRepository.AddUser(
                new User {
                    Name = user.Name,
                    Age = user.Age,
                    Email = user.Email,
                    CreatedAt = DateTime.Now,
                    Id = 0
                });
        }

        public void DeleteUser(int userID)
        {
            _userRepository.DeleteUser(userID);
        }

        public User GetUser(int userID)
        {
            return _userRepository.GetUser(userID);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }
    }
}
