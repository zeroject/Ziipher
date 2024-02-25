using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfrastructure
{
    public class UserRepository : IUserRepository
    {
        private DbContextOptions<DbContext> _options;
        public UserRepository()
        {
            _options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase("UserDB").Options;
        }

        public void AddUser(User user)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                _ = context.Add(user);
                context.SaveChanges();
            }
        }

        public void DeleteUser(int userID)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                User userToUpdate = context.Users.Find(userID);
                _ = context.Users.Remove(userToUpdate);
                context.SaveChanges();
            }
        }

        public User GetUser(int userID)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                return context.Users.Where(c => c.Id == userID).FirstOrDefault();
            }
        }

        public void UpdateUser(User user)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                User userToUpdate = context.Users.Find(user.Id);
                userToUpdate.Age = user.Age;
                userToUpdate.Email = user.Email;
                userToUpdate.Name = user.Name;
                _ = context.Users.Update(userToUpdate);
                context.SaveChanges();
            }
        }
    }
}
