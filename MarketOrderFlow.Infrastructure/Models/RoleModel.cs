using Microsoft.AspNetCore.Identity;

namespace MarketOrderFlow.Infrastructure.Models;

public class RoleModel
    :IdentityRole<string>
{
    public string Name { get; set; }
}

