using CraftParty.Application.Models.Authentication;
using CraftParty.Contracts.Authentication;
using ErrorOr;
using MediatR;

namespace CraftParty.Application.Handlers.RefreshToken;

public record RefreshTokenCommand(TokenRequestModel TokenRequestModel) : IRequest<ErrorOr<TokenRefreshResult>>;