namespace ItemsMicroservice.Application.Items.UpdateItem;

public sealed record UpdateItemRequest(string Code, string Name, string Notes, string Color);
