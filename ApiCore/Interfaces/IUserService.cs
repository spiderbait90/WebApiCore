using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCore.Data.EntityModels;

namespace ApiCore.Interfaces
{
    public interface IUserService
    {
        Task<User> Create(User user, string password);

        Task<User> Authenticate(string modelUsername, string modelPassword);

        User GetById(int id);

        string GetToken(User user);
    }
}
