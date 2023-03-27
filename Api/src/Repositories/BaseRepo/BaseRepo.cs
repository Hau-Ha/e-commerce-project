using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Api.src.Database;
using Api.src.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.src.Repositories.BaseRepo
{
    
    public class BaseRepo<T> : IBaseRepo<T>
        where T : BaseModel
    {
        protected readonly DatabaseContext _context;

        public BaseRepo(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<T?> CreateOneAsync(T create)
        {
            await _context.Set<T>().AddAsync(create);
            await _context.SaveChangesAsync();
            return create;
        }

        public async Task<bool> DeleteOneAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if(entity is null)
            {
                return false;
            }else
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(QueryOptions options)
        {
            var query = _context.Set<T>().AsNoTracking();
            if (options.Sort.Trim().Length > 0)
            {
                if (query.GetType().GetProperty(options.Sort) != null) //confirm if the "Sort" is a property of current entity or not
                {
                    query.OrderBy(e => e.GetType().GetProperty(options.Sort));
                } // check out this one to dynamically  sort data
                query.Take(options.Limit).Skip(options.Skip);
            }
            return await query.ToArrayAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            /* findasync: return 1 entity -> find from cache -> find from db. Only work with primary property
            where + ToListAsync() : return an Ienumerable
            firstordefaultasync:  return 1 entity -> find in db -> input condition*/
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateOneAsync(string id, T update)
        {
            /* _context.Entry(update).State = EntityState.Modified; */
            var entity = update;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
