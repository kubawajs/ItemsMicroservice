using ItemsMicroservice.Core.Exceptions;

namespace ItemsMicroservice.Application.Exceptions;

public sealed class InvalidColorException : ItemsMicroserviceException
{
    public InvalidColorException(string color) : base($"Color with name {color} does not exists.")
    {
    }
}
