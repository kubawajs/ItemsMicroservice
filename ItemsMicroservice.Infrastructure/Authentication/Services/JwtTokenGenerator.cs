using ItemsMicroservice.Infrastructure.Authentication.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ItemsMicroservice.Infrastructure.Authentication.Services;

internal sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public JwtTokenGenerator(
        JwtSettings jwtSettings,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _jwtSettings = jwtSettings;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<string> GenerateToken(IdentityUser user)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var claims = await GetClaimsAsync(user);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = claims,
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private async Task<ClaimsIdentity> GetClaimsAsync(IdentityUser user)
    {
        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.UserName),
            new Claim("id", user.Id)
        };

        var userClaims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);
        claims.AddRange(userClaims);
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
            var role = await _roleManager.FindByNameAsync(userRole);
            if (role != null)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }
        }

        return new ClaimsIdentity(claims);
    }
}