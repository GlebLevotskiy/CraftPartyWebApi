using AutoMapper;
using CraftParty.Application.Models.Authentication;
using CraftParty.Application.Models.Users;
using CraftParty.Contracts.Authentication;
using CraftParty.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CraftParty.Application.Mapping;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<RegisterRequestModel, RegisterModel>();
        CreateMap<LoginRequestModel, LoginModel>();

        CreateMap<RegisterModel, IdentityUser>()
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.EmailConfirmed, opt => opt.MapFrom(s => true));

        CreateMap<RegisterModel, User>()
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName));

        CreateMap<User, AuthenticationResult>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));

        CreateMap<User, UserModel>()
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName));

        CreateMap<TokensDataModel, TokenRefreshResult>()
            .ForMember(d => d.Token, opt => opt.MapFrom(s => s.JwtToken))
            .ForMember(d => d.RefreshToken, opt => opt.MapFrom(s => s.RefreshToken));

        CreateMap<TokenRequestModel, RefreshTokenModel>()
            .ForMember(d => d.Token, opt => opt.MapFrom(s => s.Token))
            .ForMember(d => d.RefreshToken, opt => opt.MapFrom(s => s.RefreshToken));
    }
}