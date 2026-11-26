using Golf.Shop.Application.Domains;

namespace Golf.Shop.Application.Abstractions;

public interface IStorage
{
    Task AddAsync(RepairOrder order, CancellationToken cancellationToken = default);
    Task<List<RepairOrder>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RepairOrder?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}


