using Golf.Shop.Application.Enums;
using Golf.Shop.Application.Exceptions;

namespace Golf.Shop.Application.Domains;

public class RepairService
{
    public RepairService(ClubType clubType, RepairServiceType type)
    {
        ServiceId = Guid.NewGuid();
        ClubType = clubType;
        Status = RepairServiceStatus.Requested;
        Type = type;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        Price = CalculatePrice();
    }

    public Guid ServiceId { get; set; }
    public decimal Price { get; set; }
    public ClubType ClubType { get; set; }
    public RepairServiceStatus Status { get; set; }
    public RepairServiceType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public decimal CalculatePrice()
    {
        var basePrice = Type switch
        {
            RepairServiceType.Regrip => 29.99m,
            RepairServiceType.Reshaft => 49.99m,
            RepairServiceType.Repair => 79.99m,
            RepairServiceType.Other => 39.99m,
            _ => throw new InvalidOperationException("Unknown repair service type.")
        };

        var clubPrice = ClubType switch
        {
            ClubType.Driver => 10m,
            ClubType.FairwayWood => 8m,
            ClubType.Hybrid => 7m,
            ClubType.Iron => 5m,
            ClubType.Putter => 10m,
            ClubType.Wedge => 6m,
            _ => throw new InvalidOperationException("Unknown club type.")
        };
        return basePrice + clubPrice;
    }

    public void Begin()
    {
        if (Status == RepairServiceStatus.Cancelled || Status == RepairServiceStatus.Completed)
            throw new InvalidServiceStateException($"Cannot begin a repair service that is {Status}.");

        if (Status == RepairServiceStatus.InProgress) return;
        Status = RepairServiceStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == RepairServiceStatus.Completed)
            throw new InvalidServiceStateException($"Cannot cancel a repair service that is {Status}.");

        if (Status == RepairServiceStatus.Cancelled) return;
        Status = RepairServiceStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Status == RepairServiceStatus.Cancelled)
            throw new InvalidServiceStateException($"Cannot complete a repair service that is {Status}.");

        if (Status == RepairServiceStatus.Completed) return;
        Status = RepairServiceStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }
}