using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Api.src.Repositories.BaseRepo
{
    public interface IBaseRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync(QueryOptions options);
        Task<T?> GetByIdAsync(string id);
        Task<T> UpdateOneAsync(string id, T update);
        Task<bool> DeleteOneAsync(string id);
        Task<T?> CreateOneAsync (T create);
    }

    public class QueryOptions
    {
        public string Sort { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public SortBy SortBy { get; set; }
        public int Limit { get; set; } = 30;
        public int Skip { get; set; } = 0;
    }

    public enum SortBy
    {
        ASC,
        DESC
    }
}
