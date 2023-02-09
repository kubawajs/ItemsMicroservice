using ItemsMicroservice.Application.Colors.Colors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ItemsMicroservice.Api.Modules;

internal static class ColorsModule
{
    public static void MapColorsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/colors", [ResponseCache(Duration = 1)] async (IMediator mediator) =>
        {
            var query = new GetColorsQuery();
            var response = await mediator.Send(query);
            return response != null ? Results.Ok(response) : Results.NotFound();
        })
        .RequireAuthorization()
        .Produces<IEnumerable<GetColorResponse>>()
        .ProducesProblem(statusCode: 400)
        .WithName("GetColors");
    }
}