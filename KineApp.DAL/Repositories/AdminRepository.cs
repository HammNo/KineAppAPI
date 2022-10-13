using KineApp.BLL.Interfaces;
using KineApp.DAL.Contexts;
using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.EF.Repository;

namespace KineApp.DAL.Repositories
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(KineAppContext context) : base(context)
        {
        }
    }
}
