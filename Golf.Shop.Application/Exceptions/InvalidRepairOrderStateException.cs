namespace Golf.Shop.Application.Exceptions;

public class InvalidRepairOrderStateException : DomainException
{
    // Creates a new InvalidRepairOrderStateException with a custom message
    public InvalidRepairOrderStateException(string message) : base(message)
    {
    }
}