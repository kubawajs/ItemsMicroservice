﻿using ItemsMicroservice.Core.Domain;
using ItemsMicroservice.Core.Exceptions;

namespace ItemsMicroservice.Infrastructure.Exceptions;

internal sealed class ItemNotFoundException : ItemsMicroserviceException
{
    public ItemNotFoundException(string itemCode) 
        : base($"Item with code {itemCode} was not found.")
    {
    }
}
