using AutoMapper;
using CraftParty.Application.Interfaces.Authentication;
using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Models.Authentication;
using ErrorOr;
using MediatR;

namespace CraftParty.Application.Handlers.Login;

public class LoginHandler : IRequestHandler<LoginCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public LoginHandler(
        IAuthenticationService authenticationService,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var loginResult = await _authenticationService.Login(_mapper.Map<LoginModel>(command.RequestModel));

        if (!loginResult.IsError)
        {
            await _unitOfWork.CompleteAsync();
        }

        return loginResult;
    }
}