using System.Security.Claims;
using Api.src.DTOs;
using Api.src.Models;
using Api.src.Services.BaseService;
using Api.src.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
namespace Api.src.Controllers
{
    public class UserController : BaseController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        private readonly IAuthorizationService _authorizationService;
        public UserController(IUserService service, IAuthorizationService authorizationService)
            : base(service)
        {
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        public override async Task<ActionResult<UserReadDto?>> CreateOne(UserCreateDto create)
        {
            return await base.CreateOne(create);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public override async Task<ActionResult<bool>> DeleteOne(string id)
        {
            return Ok(await _service.DeleteOneAsync(id));
        }
        [HttpPut("{id}")]
        public override async Task<ActionResult<UserReadDto>> UpdateOne(string id, UserUpdateDto update)
        {
            var authorization = await _authorizationService.AuthorizeAsync(HttpContext.User, id, "AdminOrOwner");
            if (authorization.Succeeded)
            {
                return Ok(await _service.UpdateOneAsync(id, update));
            }
            return Forbid();
        }



        /*         [HttpGet("profile")]
               public async Task<ActionResult<UserReadDto>> GetProfile()
               {
                   var authenticatedUser = HttpContext.User;
                   var id = authenticatedUser.FindFirstValue(ClaimTypes.NameIdentifier);
                   var user = _service.GetByIdAsync(id!);
                   return Ok(user);
               }*/

    }
}
