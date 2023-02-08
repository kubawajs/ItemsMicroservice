using ItemsMicroservice.Infrastructure.Authentication.Models;

namespace ItemsMicroservice.Infrastructure.Authentication.Services;

public interface IIdentityService
{
    Task<AuthenticationResult> LoginAsync(string username, string password);
}
