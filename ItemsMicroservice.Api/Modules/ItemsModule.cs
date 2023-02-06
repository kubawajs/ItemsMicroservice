﻿using ItemsMicroservice.Application.Items.CreateItem;
using ItemsMicroservice.Application.Items.GetItem;
using ItemsMicroservice.Application.Items.GetItems;
using ItemsMicroservice.Application.Items.UpdateItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ItemsMicroservice.Api.Modules;

internal static class ItemsModule
{
    public static void MapItemsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/items", (IMediator mediator) =>
        {
            var query = new GetItemsQuery();
            var response = mediator.Send(query);
            return response != null ? Results.Ok(response) : Results.NotFound();
        })
        .Produces<IEnumerable<GetItemResponse>>()
        .ProducesProblem(statusCode: 400)
        .WithName("GetItems");

        app.MapGet("/api/items/{code}", (IMediator mediator, string code) =>
        {
            var query = new GetItemQuery(code);
            var response = mediator.Send(query);
            return response != null ? Results.Ok(response) : Results.NotFound();
        })
        .Produces<GetItemResponse>()
        .ProducesProblem(statusCode: 400)
        .WithName("GetItemByCode");

        app.MapPost("/api/items", (IMediator mediator, CreateItemRequest request) =>
        {
            var command = new CreateItemCommand(request.Code, request.Name, request.Notes, request.Color);
            var response = mediator.Send(command);
            return response != null ? Results.CreatedAtRoute("GetItemByCode", new { code = request.Code }) : Results.BadRequest();
        })
        .ProducesProblem(statusCode: 400)
        .WithName("AddItem");

        app.MapPut("/api/items", (IMediator mediator, UpdateItemRequest request) =>
        {
            var command = new UpdateItemCommand(request.Code, request.Name, request.Notes, request.Color);
            var response = mediator.Send(command);
            return response != null ? Results.NoContent() : Results.BadRequest();
        })
        .ProducesProblem(statusCode: 400)
        .WithName("UpdateItem");
    }
}