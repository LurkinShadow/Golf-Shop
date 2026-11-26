using Golf.Shop.Application.Abstractions;
using Golf.Shop.Application.Domains;
using Golf.Shop.Infrastructure.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Golf.Shop.Infrastructure.Repositories;

public class Storage : IStorage
{
    private readonly RepairOrderContext _dbContext;
    
    public Storage(RepairOrderContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Create and Store Orders
    public async Task AddAsync(RepairOrder order, CancellationToken cancellationToken = default)
    {
        await _dbContext.RepairOrders.AddAsync(order, cancellationToken);
    } 

    //Fetch all RepairOrders 
    public async Task<List<RepairOrder>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.RepairOrders
            .Include(r => r.Services)
            .ToListAsync(cancellationToken);
    }

    //Fetch by individual OrderId
    public async Task<RepairOrder?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RepairOrders
            .Include(r => r.Services)
            .FirstOrDefaultAsync(o => o.OrderId == orderId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}