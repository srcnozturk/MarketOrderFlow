using Microsoft.AspNetCore.Identity;

namespace MarketOrderFlow.Infrastructure.Models;

public class UserModel : IdentityUser
{
    public UserModel(string userName) : base(userName) { }
    public string Name { get; set; }
    public string Surname { get; set; }
}
