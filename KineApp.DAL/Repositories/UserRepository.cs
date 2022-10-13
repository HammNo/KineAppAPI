using KineApp.BLL.Interfaces;
using KineApp.DAL.Contexts;
using KineApp.DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.EF.Repository;

namespace KineApp.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(KineAppContext context) : base(context)
        {
        }

        public IEnumerable<User> FindUsersWithAppointmentsByCriteria(
            string? firstName,
            string? lastName,
            string? email,
            string? phoneNumber,
            int offset = 0,
            int limit = 10
        )
        {
            return _entities.Include(u => u.TimeSlots)
                            .ToList()
                            .Where(u => firstName == null || u.FirstName.ToLower().Contains(firstName.ToLower()))
                            .Where(u => lastName == null || u.LastName.ToLower().Contains(lastName.ToLower()))
                            .Where(u => email == null || ((u.Email != null) && u.Email.ToLower().Contains(email.ToLower())))
                            .Where(u => phoneNumber == null || u.PhoneNumber.Contains(phoneNumber))
                            .Skip(offset)
                            .Take(limit);
        }

        public IEnumerable<User> FindAllRelatedUsers(string? email, string? phoneNumber)
        {
            return _entities.Where(u => (email != null && u.Email != null && u.Email.ToLower() == email.ToLower()) 
                                        || (phoneNumber != null && u.PhoneNumber.Contains(phoneNumber)));
        }

        public User? FindOneWithAppointements(Guid id)
        {
            return _entities.Include(u => u.TimeSlots)
                            .FirstOrDefault(u => u.Id == id);
        }

        public bool ExistUser(User user)
        {
            return _entities.Any(u => user.FirstName.Replace(" ", "").ToLower() == u.FirstName.Replace(" ", "").ToLower()
                                      && user.LastName.Replace(" ", "").ToLower() == u.LastName.Replace(" ", "").ToLower()
                                      && (user.Email == null || ((u.Email != null) && user.Email.ToLower() == u.Email.ToLower()))
                                      && user.PhoneNumber == u.PhoneNumber
                                      && user.Gender == u.Gender);
        }
    }
}
