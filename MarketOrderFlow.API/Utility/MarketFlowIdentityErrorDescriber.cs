using MarketOrderFlow.API.Properties;
using Microsoft.AspNetCore.Identity;

namespace MarketOrderFlow.API;

class MarketFlowIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateRoleName(string role) => new()
    {
        Code = Errors_Identity.DuplicateRoleName,
        Description = string.Format(Errors_Identity.DuplicateRoleNameMessage, role)
    };
    public override IdentityError DuplicateEmail(string email) => new()
    {
        Code = Errors_Identity.DuplicateEmail,
        Description = string.Format(Errors_Identity.DuplicateEmailMessage, email)
    };
    public override IdentityError DuplicateUserName(string userName) => new()
    {
        Code = Errors_Identity.DuplicateUserName,
        Description = string.Format(Errors_Identity.DuplicateUserNameMessage, userName)
    };
    public override IdentityError InvalidEmail(string email) => new()
    {
        Code = Errors_Identity.InvalidEmail,
        Description = string.Format(Errors_Identity.InvalidEmailMessage, email)
    };
    public override IdentityError InvalidRoleName(string role) => new()
    {
        Code = Errors_Identity.InvalidRoleName,
        Description = string.Format(Errors_Identity.InvalidRoleNameMessage, role)
    };
    public override IdentityError InvalidToken() => new()
    {
        Code = Errors_Identity.InvalidToken,
        Description = Errors_Identity.InvalidTokenMessage
    };
    public override IdentityError InvalidUserName(string userName) => new()
    {
        Code = Errors_Identity.InvalidUserName,
        Description = string.Format(Errors_Identity.InvalidUserNameMessage, userName)
    };
    public override IdentityError LoginAlreadyAssociated() => new()
    {
        Code = Errors_Identity.LoginAlreadyAssociated,
        Description = Errors_Identity.LoginAlreadyAssociatedMessage
    };
    public override IdentityError PasswordMismatch() => new()
    {
        Code = Errors_Identity.PasswordMismatch,
        Description = Errors_Identity.PasswordMismatchMessage
    };
    public override IdentityError PasswordRequiresDigit() => new()
    {
        Code = Errors_Identity.PasswordRequiresDigit,
        Description = Errors_Identity.PasswordRequiresDigitMessage
    };
    public override IdentityError PasswordRequiresLower() => new()
    {
        Code = Errors_Identity.PasswordRequiresLower,
        Description = Errors_Identity.PasswordRequiresLowerMessage
    };
    public override IdentityError PasswordRequiresNonAlphanumeric() => new()
    {
        Code = Errors_Identity.PasswordRequiresNonAlphanumeric,
        Description = Errors_Identity.PasswordRequiresNonAlphanumericMessage
    };
    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) => new()
    {
        Code = Errors_Identity.PasswordRequiresUniqueChars,
        Description = string.Format(Errors_Identity.PasswordRequiresUniqueCharsMessage, uniqueChars)
    };
    public override IdentityError PasswordRequiresUpper() => new()
    {
        Code = Errors_Identity.PasswordRequiresUpper,
        Description = Errors_Identity.PasswordRequiresUpperMessage
    };
    public override IdentityError PasswordTooShort(int length) => new()
    {
        Code = Errors_Identity.PasswordTooShort,
        Description = string.Format(Errors_Identity.PasswordTooShortMessage, length)
    };
    public override IdentityError UserAlreadyHasPassword() => new()
    {
        Code = Errors_Identity.UserAlreadyHasPassword,
        Description = Errors_Identity.UserAlreadyHasPasswordMessage
    };
    public override IdentityError UserAlreadyInRole(string role) => new()
    {
        Code = Errors_Identity.UserAlreadyInRole,
        Description = string.Format(Errors_Identity.UserAlreadyInRoleMessage, role)
    };
    public override IdentityError UserNotInRole(string role) => new()
    {
        Code = Errors_Identity.UserNotInRole,
        Description = string.Format(Errors_Identity.UserNotInRoleMessage, role)
    };
    public override IdentityError UserLockoutNotEnabled() => new()
    {
        Code = Errors_Identity.UserLockoutNotEnabled,
        Description = Errors_Identity.UserLockoutNotEnabledMessage
    };
    public override IdentityError RecoveryCodeRedemptionFailed() => new()
    {
        Code = Errors_Identity.RecoveryCodeRedemptionFailed,
        Description = Errors_Identity.RecoveryCodeRedemptionFailedMessage
    };
    public override IdentityError ConcurrencyFailure() => new()
    {
        Code = Errors_Identity.ConcurrencyFailure,
        Description = Errors_Identity.ConcurrencyFailureMessage
    };
    public override IdentityError DefaultError() => new()
    {
        Code = Errors_Identity.DefaultError,
        Description = Errors_Identity.DefaultErrorMessage
    };
}
