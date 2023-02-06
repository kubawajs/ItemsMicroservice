namespace ItemsMicroservice.Application.Items.CreateItem;

public sealed record CreateItemRequest(string Code, string Name, string Notes, string Color);