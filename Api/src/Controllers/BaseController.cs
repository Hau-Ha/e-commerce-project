using Api.src.Repositories.BaseRepo;
using Api.src.Services.BaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.src.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]s")]
    public class BaseController<T, TReadDto, TCreateDto, TUpdateDto> : ControllerBase
    {
        protected readonly IBaseService<T, TReadDto, TCreateDto, TUpdateDto> _service;

        public BaseController(IBaseService<T, TReadDto, TCreateDto, TUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<TReadDto>>> GetAll(
            [FromQuery] QueryOptions options
        )
        {
            return Ok(await _service.GetAllAsync(options));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TReadDto?>> GetById([FromRoute] string id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost()]
        public async virtual Task<ActionResult<TReadDto?>> CreateOne(TCreateDto create)
        {
            var result = await _service.CreateOneAsync(create);
            return Ok(result);
        }
    }
}
