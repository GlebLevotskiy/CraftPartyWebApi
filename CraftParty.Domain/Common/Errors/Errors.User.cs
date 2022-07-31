using ErrorOr;

namespace CraftParty.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error CreationIssue => Error.Failure(
            code: nameof(CreationIssue),
            description: "There was a issue creating the user");
    }
}