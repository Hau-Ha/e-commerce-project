using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.src.Helpers;
using Api.src.Repositories.BaseRepo;
using AutoMapper;

namespace Api.src.Services.BaseService
{
    public class BaseService<T, TReadDto, TCreateDto, TUpdateDto>
        : IBaseService<T, TReadDto, TCreateDto, TUpdateDto>
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseRepo<T> _repo;

        public BaseService(IMapper mapper, IBaseRepo<T> repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        /* UpdateModel()
        {
            model.firstname = firstname
            model.email = email
        }
        mapper looks for same name of property in 2 entity to transform
        mappper.Map(model) = entity
        */
        public virtual async Task<TReadDto> CreateOneAsync(TCreateDto create)
        {
            var entity = _mapper.Map<TCreateDto, T>(create);
            var result = await _repo.CreateOneAsync(entity);
            if (result is null)
            {
                throw ServiceException.BadRequest("Data is not valid");
            }
            return _mapper.Map<T, TReadDto>(result);
        }

        public async Task<bool> DeleteOneAsync(string id)
        {
            var result = await _repo.DeleteOneAsync(id);
            if (!result) throw ServiceException.NotFound();
            return result;
        }

        public async Task<IEnumerable<TReadDto>> GetAllAsync(QueryOptions options)
        {
            var data = await _repo.GetAllAsync(options);
            return _mapper.Map<IEnumerable<T>, IEnumerable<TReadDto>>(data);
        }

        public async Task<TReadDto?> GetByIdAsync(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)
            {
                throw ServiceException.NotFound();
            }
            return _mapper.Map<T, TReadDto>(entity);
        }

        public async Task<TReadDto> UpdateOneAsync(string id, TUpdateDto update)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)
            {
                throw ServiceException.NotFound();
            }
            var result = await _repo.UpdateOneAsync(id, _mapper.Map<TUpdateDto, T>(update));
            return _mapper.Map<T, TReadDto>(result);
        }
    }
}
