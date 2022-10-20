using IndividualProject.DTO;
using IndividualProject.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IndividualProject.DAL
{
    public class UserEF : IUser
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<IdentityUser> _userManager;

        public UserEF(UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }
        public async Task<UserGetDTO> Authenticate(AddUserDTO user)
        {
            var currUser = await _userManager.FindByNameAsync(user.Username);
            var userResult = await _userManager.CheckPasswordAsync(currUser, user.Password);
            if (!userResult)
                throw new Exception($"Authenticate Failed");

            UserGetDTO userToken = new UserGetDTO
            {
                Username = user.Username
            };

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            var TokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = TokenHandler.CreateToken(tokenDescriptor);
            userToken.Token = TokenHandler.WriteToken(token);
            return userToken;
        }

        public IEnumerable<UserGetDTO> GetUsers()
        {
            var users = new List<UserGetDTO>();
            foreach(var user in _userManager.Users)
            {
                users.Add(new UserGetDTO
                {
                    Username = user.UserName
                });
            }
            return users;
        }

        public async Task Registration(AddUserDTO user)
        {
            try
            {
                var newUser = new IdentityUser
                {
                    UserName = user.Username,
                    Email = user.Username
                };
                var result = await _userManager.CreateAsync(newUser,user.Password);
                if (!result.Succeeded)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach(var error in result.Errors)
                    {
                        sb.Append($"{error.Code} - {error.Description} \n"); 
                    }
                    throw new Exception(sb.ToString());
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
