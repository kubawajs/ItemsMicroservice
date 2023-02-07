namespace ItemsMicroservice.Infrastructure.Models;

public class AuthenticationResult
{
    public AuthenticationResult(IEnumerable<string> errors) => Errors = errors;

    public AuthenticationResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}