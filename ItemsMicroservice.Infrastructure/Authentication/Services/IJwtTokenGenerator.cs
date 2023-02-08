using Microsoft.AspNetCore.Identity;

namespace ItemsMicroservice.Infrastructure.Authentication.Services;

public interface IJwtTokenGenerator
{
    Task<string> GenerateToken(IdentityUser user);
}
