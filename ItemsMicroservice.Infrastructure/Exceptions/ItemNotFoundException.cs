using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Core.Exceptions;

namespace ItemsMicroservice.Infrastructure.Exceptions;

internal sealed class ItemNotFoundException : ItemsMicroserviceException
{
    public ItemNotFoundException(Item item) 
        : base($"Item with code {item.Code} and name {item.Name} was not found.")
    {
    }
}
