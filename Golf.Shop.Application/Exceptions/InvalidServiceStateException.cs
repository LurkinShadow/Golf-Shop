namespace Golf.Shop.Application.Exceptions;

public class InvalidServiceStateException : DomainException
{
    // Creates a new InvalidServiceStateException with a custom message.
    public InvalidServiceStateException(string message) : base(message)
    {
    }
}