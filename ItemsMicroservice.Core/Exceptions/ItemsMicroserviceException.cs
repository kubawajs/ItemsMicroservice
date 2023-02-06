namespace ItemsMicroservice.Core.Exceptions;

/// <summary>
/// Base class for all exceptions in the application
/// </summary>
public class ItemsMicroserviceException : Exception
{
	public ItemsMicroserviceException(string message) : base(message) { }
}
