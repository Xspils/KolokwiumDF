using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KolokwiumDF.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly YourDbContext _context;

    public DiscountRepository(YourDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Discount>> GetByClientIdAsync(int clientId)
    {
        return await _context.Discounts.Where(d => d.IdClient == clientId).ToListAsync();
    }

    public async Task<Discount> GetActiveDiscountByClientIdAsync(int clientId)
    {
        var currentDate = DateTime.UtcNow;
        return await _context.Discounts
            .Where(d => d.IdClient == clientId && d.DateFrom <= currentDate && d.DateTo >= currentDate)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Discount discount)
    {
        _context.Discounts.Add(discount);
        await _context.SaveChangesAsync();
    }
}