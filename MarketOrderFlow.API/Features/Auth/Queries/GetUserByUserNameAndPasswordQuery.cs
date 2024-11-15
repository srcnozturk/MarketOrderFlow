namespace MarketOrderFlow.API.Features.Auth.Queries;

public readonly record struct GetUserByUserNameAndPasswordQuery(string UserName,string Password);

