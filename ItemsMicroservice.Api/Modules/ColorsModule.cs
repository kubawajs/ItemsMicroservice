using ItemsMicroservice.Application.Colors.Colors;
using MediatR;

namespace ItemsMicroservice.Api.Modules;

internal static class ColorsModule
{
    public static void MapColorsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/colors", async (IMediator mediator) =>
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