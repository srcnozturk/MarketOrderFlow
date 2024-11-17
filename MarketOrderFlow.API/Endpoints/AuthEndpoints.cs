namespace MarketOrderFlow.API.Endpoints;

static class AuthEndpoints
{
    public static RouteGroupBuilder MapAuthApi(this RouteGroupBuilder group)
    {
        group.MapGet("/roles", GetRoles);
        group.MapPost("/user", AddNewUser);
        group.MapPost("/roles", AddNewRole);
        group.MapPost("/login", UserLogin);
        group.MapGet("/users", GetAllUsers);
        return group;
    }
    static async Task<Results<Ok<UserModel[]>, ProblemHttpResult>> GetAllUsers(
         UserManager<UserModel> userManager)
    {
        try
        {
            var users = await userManager.Users.ToListAsync();
            return TypedResults.Ok(users.ToArray());

        }
        catch (Exception e)
        {
            ProblemDetails details = new()
            {
                Status = 400,
                Detail = e.Message,
            };

            return TypedResults.Ok(new UserModel[0]);
        }
    }
    static async Task<Results<Ok<RoleModel[]>, ProblemHttpResult>> GetRoles(
       RoleManager<IdentityRole> roleManager)
    {
        try
        {
            var roleNames = await roleManager.Roles.Select(r => r.Name).ToArrayAsync();
            var roles = roleNames.Select(name => new RoleModel { Name = name }).ToArray();
            return TypedResults.Ok(roles);
        }
        catch (Exception e)
        {
            var details = new ProblemDetails { Status = 400, Detail = e.Message, };
            return TypedResults.Ok(new RoleModel[0]);
        }
    }
    static async Task<Results<Ok<string>, ProblemHttpResult>> UserLogin(
       [FromBody] GetUserByUserNameAndPasswordQuery query,
       UserManager<UserModel> userManager,
       IConfiguration configuration,
       HttpContext context)
    {
        try
        {
            string? identity = query.UserName;
            

            Func<string, Task<UserModel?>> find = identity == query.UserName ?
                userManager.FindByNameAsync :
                userManager.FindByEmailAsync;

            UserModel? user = await find(identity);
            
            var passwordIsTrue= await userManager.CheckPasswordAsync(user, query.Password);
            if(!passwordIsTrue) return TypedResults.Problem(statusCode: 400, detail: Errors_Identity.UserPasswordMismatch);

            var authClaims = await GetJWTClaimsForUser(userManager, user);
            string jwt = GenerateNewJsonWebToken(authClaims);

            return TypedResults.Ok(jwt);
        }
        catch (Exception e)
        {
            var details = new ProblemDetails { Status = 401, Detail = e.Message, };
            return TypedResults.Problem(details);
        }
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
    static async Task<IEnumerable<Claim>> GetJWTClaimsForUser(
        UserManager<UserModel> userManager, UserModel user)
    {
        IList<string> roles = await userManager.GetRolesAsync(user);
        var now = DateTimeOffset.Now;
        long iat = now.ToUnixTimeSeconds();
        long exp = now.AddMinutes(1).ToUnixTimeSeconds();

        return [
            new Claim("iss", MarketOrderFlowConfiguration.Authentication.Issuer),
            new Claim("aud", MarketOrderFlowConfiguration.Authentication.Audience),
            new Claim("exp", $"{exp}", ClaimValueTypes.Integer),
            new Claim("iat", $"{iat}", ClaimValueTypes.Integer),
            new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? string.Empty),
            new Claim(ClaimTypes.GivenName, user.Name ?? string.Empty),
            new Claim(ClaimTypes.Surname, user.Surname ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            .. roles.Select(role => new Claim(ClaimTypes.Role, role))
        ];
    }
    static string GenerateNewJsonWebToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(MarketOrderFlowConfiguration.Authentication.SecurityKey));

        var token = new JwtSecurityToken(
            claims: claims, signingCredentials: new(securityKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

