using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.EF.Repository;

namespace KineApp.BLL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> FindUsersWithAppointmentsByCriteria(string? firstName, string? lastName, string? email, string? phoneNumber, int offset = 0, int limit = 10);
        IEnumerable<User> FindAllRelatedUsers(string? email, string? phoneNumber);
        User? FindOneWithAppointements(Guid id);
        bool ExistUser(User user);
    }
}
