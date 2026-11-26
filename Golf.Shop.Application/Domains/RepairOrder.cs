using Golf.Shop.Application.Enums;
using Golf.Shop.Application.Exceptions;

namespace Golf.Shop.Application.Domains;

public class RepairOrder
{
    private RepairOrder()
    {
        CustomerEmail = string.Empty;
        CustomerMessage = string.Empty;
        Services = new List<RepairService>();
        InternalNotes = string.Empty;
    }

    //Customer Created Repair Order
    public RepairOrder(string customerEmail, string customerMessage)
    {
        if (string.IsNullOrWhiteSpace(customerEmail))
            throw new InvalidRepairOrderException("Customer email is required.");

        if (string.IsNullOrWhiteSpace(customerMessage))
            throw new InvalidRepairOrderException("Customer message is required.");
        OrderId = Guid.NewGuid();
        CustomerEmail = customerEmail;
        Status = RepairOrderStatus.New;
        CustomerMessage = customerMessage;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Services = new List<RepairService>();
        InternalNotes = string.Empty;
    }

    public Guid OrderId { get; set; }
    public string CustomerEmail { get; set; }
    public RepairOrderStatus Status { get; set; }
    public string CustomerMessage { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<RepairService> Services { get; set; }
    public string InternalNotes { get; set; }

    //Add RepairService to Repair Order
    public void AddService(ClubType clubType, RepairServiceType type)
    {
        if (Status == RepairOrderStatus.Completed || Status == RepairOrderStatus.Cancelled)
            throw new InvalidRepairOrderStateException($"Cannot add service to a repair order that is {Status}.");
        
        var service = new RepairService(clubType, type);
        Services.Add(service);
        UpdatedAt = DateTime.UtcNow;
    }

    //Updated Repair Order Status to In Progress
    public void Begin()
    {
        if (Status == RepairOrderStatus.Completed || Status == RepairOrderStatus.Cancelled)
            throw new InvalidRepairOrderStateException($"Cannot begin a repair order that is {Status}.");

        if (Status == RepairOrderStatus.InProgress) return;
        Status = RepairOrderStatus.InProgress;
        foreach (var service in Services) service.Begin();
        UpdatedAt = DateTime.UtcNow;
    }

    //Cancel entire repair order
    public void Cancel()
    {
        if (Status == RepairOrderStatus.Completed)
            throw new InvalidRepairOrderStateException($"Cannot cancel a repair order that is {Status}.");

        if (Status == RepairOrderStatus.Cancelled) return;

        Status = RepairOrderStatus.Cancelled;
        foreach (var service in Services) service.Cancel();
        UpdatedAt = DateTime.UtcNow;
    }

    //Repair Order Completed
    public void Complete()
    {
        if (Status == RepairOrderStatus.Cancelled)
            throw new InvalidRepairOrderStateException($"Cannot complete a repair order that is {Status}.");

        if (Status == RepairOrderStatus.Completed) return;
        Status = RepairOrderStatus.Completed;
        foreach (var service in Services) service.Complete();
        UpdatedAt = DateTime.UtcNow;
    }
}