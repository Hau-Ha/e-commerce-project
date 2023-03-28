using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.Models;
using Api.src.Repositories.BaseRepo;

namespace Api.src.Repositories.UserRepo
{
    public interface IUserRepo : IBaseRepo<User> 
    {
        Task<int> GetTotal ();
    }
}