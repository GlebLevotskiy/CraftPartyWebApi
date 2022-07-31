using AutoMapper;
using CraftParty.Application.Interfaces.Authentication;
using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Models.Authentication;
using ErrorOr;
using MediatR;

namespace CraftParty.Application.Handlers.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterHandler(
        IAuthenticationService authenticationService,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var registrationResult = await _authenticationService.Register(
            _mapper.Map<RegisterModel>(command.RequestModel));

        if (!registrationResult.IsError)
        {
            await _unitOfWork.CompleteAsync();
        }

        return registrationResult;
    }
}