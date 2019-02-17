/************************************************/
/*************   Trade Secret    ****************/
/************************************************/

using DotNetCoreApiClient.Helpers;
using DotNetCoreWebApi.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

#region ChangeComments
/* IssueNo - DeveloperName - Date : Comment    */
#endregion

namespace DotNetCoreWebApi.Services
{
    /// <summary>
    /// This Class to authenticate the User and generate the token.
    /// </summary>
    public class UserService : IUserService
    {
        // For production app we can store the authorized user to DB and authenticate. For testing using hardcoded values
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "SURINDER", LastName = "SINGH", Username = "SINGHS", Password = "qwerty@1234" },
            new User { Id = 1, FirstName = "DAVID", LastName = "JONES", Username = "JONESD", Password = "qwerty@1234" },
            new User { Id = 1, FirstName = "JAZ", LastName = "MIKE", Username = "MIKEJ", Password = "qwerty@1234" },
            new User { Id = 1, FirstName = "ANIL", LastName = "SHARMA", Username = "SHARMA", Password = "qwerty@1234" }
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

            // return null if user not found. Add check here if Authentication failed.
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            user.Token = GenerateJWTToken(user);

            // remove password before returning
            user.Password = null;

            return user;
        }

        /// <summary>
        /// The GenerateJWTToken method.
        /// </summary>
        private string GenerateJWTToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler;
            SecurityToken token;
          
            tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// The GetAll Method to returns all users.
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            return _users;
        }
    }
}