using CraftParty.Application.Models.Authentication;
using CraftParty.Contracts.Authentication;
using ErrorOr;
using MediatR;

namespace CraftParty.Application.Handlers.Register;

public record RegisterCommand(RegisterRequestModel RequestModel) : IRequest<ErrorOr<AuthenticationResult>>;