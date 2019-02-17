using DotNetCoreWebApi.Model;
using System.Collections.Generic;

namespace DotNetCoreWebApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

        IEnumerable<User> GetAll();
    }

}
