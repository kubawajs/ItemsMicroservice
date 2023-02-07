namespace ItemsMicroservice.Infrastructure.Settings;

public sealed class JwtSettings
{
    public static string SectionName = "JwtSettings";

    public string Secret { get; set; }
}
