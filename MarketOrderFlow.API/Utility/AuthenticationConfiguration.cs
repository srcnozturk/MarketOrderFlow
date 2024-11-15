namespace MarketOrderFlow.API.Utility;

 class AuthenticationConfiguration
{
    public string SecurityKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string ValidIssuer { get; set; } = string.Empty;
    public string[] ValidAudiences { get; set; } = [];
}
