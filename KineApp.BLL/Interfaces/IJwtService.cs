using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(string identifier, string email, string role);
    }
}
