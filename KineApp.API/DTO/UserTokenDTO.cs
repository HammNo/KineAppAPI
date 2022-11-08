using KineApp.BLL.DTO.User;

namespace KineApp.API.DTO
{
    public class UserTokenDTO
    {
        public string Token { get; set; }
        public UserDTO User { get; set; }

        public UserTokenDTO(string token, UserDTO user)
        {
            Token = token;
            User = user;
        }
    }
}
