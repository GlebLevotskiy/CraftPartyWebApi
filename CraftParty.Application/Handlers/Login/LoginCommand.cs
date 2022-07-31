using CraftParty.Application.Models.Authentication;
using CraftParty.Contracts.Authentication;
using ErrorOr;
using MediatR;

namespace CraftParty.Application.Handlers.Login;

public record LoginCommand(LoginRequestModel RequestModel) : IRequest<ErrorOr<AuthenticationResult>>;