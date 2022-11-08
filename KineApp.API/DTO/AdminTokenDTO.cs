using KineApp.BLL.DTO.Admin;

namespace KineApp.API.DTO
{
    public class AdminTokenDTO
    {
        public string Token { get; set; }
        public AdminDTO Admin { get; set; }

        public AdminTokenDTO(string token, AdminDTO admin)
        {
            Token = token;
            Admin = admin;
        }
    }
}

