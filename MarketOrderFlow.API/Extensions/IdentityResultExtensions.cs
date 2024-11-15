using Microsoft.AspNetCore.Identity;

namespace MarketOrderFlow.API.Extensions;

public static class IdentityResultExtensions
{
    public static string GetErrors(this IdentityResult result) =>
       string.Join("\n", result.Errors.Select(e => $"({e.Code}){e.Description}"));
}
