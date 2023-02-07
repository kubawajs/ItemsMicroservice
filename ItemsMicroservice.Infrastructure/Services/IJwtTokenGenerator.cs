using Microsoft.AspNetCore.Identity;

namespace ItemsMicroservice.Infrastructure.Services;

public interface IJwtTokenGenerator
{
    Task<string> GenerateToken(IdentityUser user);
}
