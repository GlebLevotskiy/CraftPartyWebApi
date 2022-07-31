using AutoMapper;
using CraftParty.Application.Interfaces.Authentication;
using CraftParty.Application.Interfaces.Data;
using CraftParty.Application.Models.Authentication;
using ErrorOr;
using MediatR;

namespace CraftParty.Application.Handlers.RefreshToken;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<TokenRefreshResult>>
{
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RefreshTokenHandler(
        IRefreshTokenService refreshTokenService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _refreshTokenService = refreshTokenService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TokenRefreshResult>> Handle(
        RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var refreshTokenResult = await _refreshTokenService.RefreshToken(
            _mapper.Map<RefreshTokenModel>(command.TokenRequestModel));

        if (!refreshTokenResult.IsError)
        {
            await _unitOfWork.CompleteAsync();
        }

        return refreshTokenResult;
    }
}