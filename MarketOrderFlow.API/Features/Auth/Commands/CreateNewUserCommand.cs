using MarketOrderFlow.Infrastructure.Models;

namespace MarketOrderFlow.API.Features.Auth.Commands;

public class CreateNewUserCommand(string userName, string password, RoleModel role)
    : UserModel(userName)
{
    public string? Password { get; set; } = password;
    public RoleModel Role { get; set; } = role;
}
