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

    if (!string.IsNullOrEmpty(options.Sort))
    {
        var property = typeof(T).GetProperty(options.Sort, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property != null)
        {
            if (options.SortBy == SortBy.ASC)
            {
                query = query.OrderBy(x => property.GetValue(x, null));
            }
            else
            {
                query = query.OrderByDescending(x => property.GetValue(x, null));
            }
        }
    }

    query = query.Skip(options.Skip).Take(options.Limit);

    return await query.ToListAsync(); 
}

    public async Task<T?> GetByIdAsync(string id)
        { 
            return await _context.Set<T>().FindAsync(id); 
        }

      public async Task<T> UpdateOneAsync(string id, T update)
{
    var entity = await _context.Set<T>().FindAsync(id);

    if (entity != null)
    {
        _context.Entry(entity).CurrentValues.SetValues(update);
        await _context.SaveChangesAsync();
    }

    return entity;
}
    }
}
