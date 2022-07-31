using ErrorOr;

namespace CraftParty.Domain.Common.Errors;

public static partial class Errors 
{
    public static class Authentication
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: nameof(DuplicateEmail),
            description: "Email is already exists");
        
        public static Error InvalidCredentials => Error.Validation(
            code: nameof(Authentication.InvalidCredentials),
            description: "Invalid credentials");
        
        public static Error InvalidToken => Error.Validation(
            code: nameof(Authentication.InvalidToken),
            description: "Invalid token");
        
        public static Error InvalidRefreshToken => Error.Validation(
            code: nameof(Authentication.InvalidRefreshToken),
            description: "Invalid refresh token");
        
        public static Error InvalidTokenExpiryDate => Error.Validation(
            code: nameof(Authentication.InvalidRefreshToken),
            description: "Invalid token expiry date");
        
        public static Error InvalidTokenJti => Error.Validation(
            code: nameof(Authentication.InvalidTokenJti),
            description: "Invalid token jti");
        
        public static Error InvalidTokenUser => Error.Validation(
            code: nameof(Authentication.InvalidTokenUser),
            description: "User for token not found");
    }
}