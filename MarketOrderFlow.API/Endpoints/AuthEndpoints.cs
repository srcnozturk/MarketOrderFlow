using MarketOrderFlow.API.Extensions;
using MarketOrderFlow.API.Features.Auth.Commands;
using MarketOrderFlow.Infrastructure.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketOrderFlow.API.Endpoints;

static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthApi(this RouteGroupBuilder group)
    {
        group.MapPost("/user", AddNewUser);
        group.MapPost("/roles", AddNewRole);
        group.MapPost("/users/{userId:guid}/roles", AddRolesToUserByUserId);
        return group;
    }
    static async Task<Results<Created, ProblemHttpResult>> AddNewUser(
        [FromBody] CreateNewUserCommand newUserCommand,
        UserManager<UserModel> userManager,
        HttpContext context)
    {
        try
        {
            IdentityResult newUserResult = await userManager.CreateAsync(newUserCommand, newUserCommand.Password);
            if (!newUserResult.Succeeded) return TypedResults.Problem(statusCode: 400, detail: newUserResult.GetErrors());

            var createdUser = await userManager.FindByIdAsync(newUserCommand.Id);
            Guid userId = Guid.Parse(createdUser.Id);

            var addrole = AddRolesToUserByUserId(userId, newUserCommand.Role, userManager);
            var createdUri = $"{context.Request.GetEncodedUrl()}/{newUserCommand.Id}";

            return TypedResults.Created(createdUri);
        }
        catch (Exception e)
        {
            var details = new ProblemDetails() { Status = 400, Detail = e.Message, };

            return TypedResults.Problem(details);
        }
    }

    static async Task<Results<Ok, ProblemHttpResult>> AddRolesToUserByUserId(
        Guid userId, [FromBody] RoleModel role, UserManager<UserModel> userManager)
    {
        try
        {
            UserModel? user = await userManager.FindByIdAsync(userId.ToString());
            var roleName = role.Name;

            var roleAssignmentResult = userManager.AddToRoleAsync(user, roleName).Result;
            if (!roleAssignmentResult.Succeeded)
                return TypedResults.Problem(statusCode: 400, detail: roleAssignmentResult.GetErrors());

            return TypedResults.Ok();
        }
        catch (Exception e)
        {
            ProblemDetails details = new() { Status = 400, Detail = e.Message, };
            return TypedResults.Problem(details);
        }
    }
    static async Task<Results<Created, ProblemHttpResult>> AddNewRole(
       [FromBody] string roleName, RoleManager<IdentityRole> roleManager)
    {
        try
        {
            var roleCreationResult = await roleManager.CreateAsync(new(roleName));
            if (!roleCreationResult.Succeeded) return TypedResults.Problem(statusCode: 400, detail: roleCreationResult.GetErrors());

            return TypedResults.Created();
        }
        catch (Exception e)
        {
            ProblemDetails details = new() { Status = 400, Detail = e.Message, };
            return TypedResults.Problem(details);
        }
    }
}

