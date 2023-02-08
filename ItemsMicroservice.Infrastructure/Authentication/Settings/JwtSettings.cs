namespace ItemsMicroservice.Infrastructure.Authentication.Settings;

public sealed class JwtSettings
{
    public static string SectionName = "JwtSettings";

    public string Secret { get; set; } = string.Empty;
}
