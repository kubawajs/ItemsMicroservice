using ItemsMicroservice.Infrastructure.Exceptions;
using ItemsMicroservice.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace ItemsMicroservice.Infrastructure.Services;

internal sealed class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public IdentityService(UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResult> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null)
        {
            throw new UserNotFoundException(username);
        }

        var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
        if (!userHasValidPassword)
        {
            throw new InvalidCredentialsException();
        }

        var jwtToken = await _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(true, jwtToken);
    }
}
