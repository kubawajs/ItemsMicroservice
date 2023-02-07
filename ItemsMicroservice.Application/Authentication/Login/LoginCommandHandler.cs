using ItemsMicroservice.Infrastructure.Services;
using MediatR;

namespace ItemsMicroservice.Application.Authentication.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService) => _identityService = identityService;

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.LoginAsync(request.Username, request.Password);
        return !response.Success
            ? new LoginResponse(request.Username, string.Join("\n", response.Errors), false)
            : new LoginResponse(request.Username, response.Message, true);
    }
}
