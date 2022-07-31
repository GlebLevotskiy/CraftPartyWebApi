using CraftParty.Application.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CraftParty.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    Task<TokensDataModel> GenerateToken(IdentityUser user);
}