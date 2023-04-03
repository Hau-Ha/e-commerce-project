using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.Database;
using Api.src.Models;
using Api.src.Repositories.BaseRepo;
using Microsoft.EntityFrameworkCore;

namespace Api.src.Repositories.UserRepo
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        private new readonly DatabaseContext _context;
        public UserRepo(DatabaseContext context) : base(context)
        {
            _context = context;
        }
        public async Task<int> GetTotal()
        {
            return await _context.Users.CountAsync();
        }
    }
}