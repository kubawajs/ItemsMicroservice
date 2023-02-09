using ItemsMicroservice.Application.Items.CreateItem;
using ItemsMicroservice.Application.Items.GetItem;
using ItemsMicroservice.Application.Items.GetItems;
using ItemsMicroservice.Application.Items.UpdateItem;
using ItemsMicroservice.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ItemsMicroservice.Api.Modules;

internal static class ItemsModule
{
    public static void MapItemsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/items", async (IMediator mediator, int? page, int? pageSize) =>
        {
            var query = new GetPaginatedItemsQuery(page, pageSize);
            var response = await mediator.Send(query);
            return response != null ? Results.Ok(response) : Results.NotFound();
        })
        .RequireAuthorization()
        .Produces<IEnumerable<GetItemResponse>>()
        .ProducesProblem(statusCode: 400)
        .WithName("GetItems");

        app.MapGet("/api/items/{code}", async (IMediator mediator, string code) =>
        {
            var query = new GetItemQuery(code);
            var response = await mediator.Send(query);
            return response != null ? Results.Ok(response) : Results.NotFound();
        })
        .RequireAuthorization()
        .Produces<GetItemResponse>()
        .ProducesProblem(statusCode: 400)
        .WithName("GetItemByCode");

        app.MapPost("/api/items", async (IMediator mediator, CreateItemRequest request) =>
        {
            var command = new CreateItemCommand(request.Code, request.Name, request.Notes, request.Color);
            var response = await mediator.Send(command);
            return response != null ? Results.CreatedAtRoute("GetItemByCode", new { code = request.Code }) : Results.BadRequest();
        })
        .RequireAuthorization(Constants.Policies.RequireAdminRole)
        .ProducesProblem(statusCode: 400)
        .WithName("AddItem");

        app.MapPut("/api/items", async (IMediator mediator, UpdateItemRequest request) =>
        {
            var command = new UpdateItemCommand(request.Code, request.Name, request.Notes, request.Color);
            var response = await mediator.Send(command);
            return response != null ? Results.NoContent() : Results.BadRequest();
        })
        .RequireAuthorization(Constants.Policies.RequireAdminRole)
        .ProducesProblem(statusCode: 400)
        .WithName("UpdateItem");
    }
}
