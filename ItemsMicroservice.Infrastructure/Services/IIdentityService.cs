using ItemsMicroservice.Infrastructure.Models;

namespace ItemsMicroservice.Infrastructure.Services;

public interface IIdentityService
{
    Task<AuthenticationResult> LoginAsync(string username, string password);
}
