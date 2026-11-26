namespace Golf.Shop.Application.Exceptions;

public class InvalidRepairOrderException : DomainException
{
    // Creates a new InvalidRepairOrderException with a custom message.
    public InvalidRepairOrderException(string message) : base(message)
    {
    }
}