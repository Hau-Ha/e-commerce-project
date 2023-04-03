using System.Security.Claims;
using Api.src.Models;
using Microsoft.AspNetCore.Authorization;

namespace Api.src.Authorization
{
    public class UpdateUserRequirement : IAuthorizationRequirement { }

    public class UpdateUserPermission : AuthorizationHandler<UpdateUserRequirement, Guid>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            UpdateUserRequirement requirement,
            Guid resource
        )
        {
            var userRole = context.User.FindFirstValue(ClaimTypes.Role);
            Console.WriteLine("conext resource : " + context.Resource);
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userRole == Role.Admin.ToString())
            {
                context.Succeed(requirement);
            }
            else if (userId == resource.ToString()) //verify if user tries to edit his own profile or not
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
