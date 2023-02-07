namespace ItemsMicroservice.Application.Authentication.Login;

public sealed record LoginResponse(string Username, string Token, bool Success);
