using ItemsMicroservice.Application.Authentication.Login;
using MediatR;

namespace ItemsMicroservice.Api.Modules;

internal static class AuthenticationModule
{
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        app.MapPost("/api/login", async (IMediator mediator, LoginRequest request) =>
        {
            var command = new LoginCommand(request.Username, request.Password);
            var response = await mediator.Send(command);
            if (response is null)
            {
                return Results.BadRequest();
            }
            return response.Success ? Results.Ok(response) : Results.Unauthorized();
        })
        .Produces<LoginResponse>()
        .ProducesProblem(statusCode: 400)
        .ProducesProblem(statusCode: 401)
        .WithName("Login");
    }
}