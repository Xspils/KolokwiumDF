using System.Collections.Generic;
using System.Threading.Tasks;

namespace KolokwiumDF.Interface;

public interface IDiscountRepository
{
	Task<IEnumerable<Discount>> GetByClientIdAsync(int clientId);
	Task<Discount> GetActiveDiscountByClientIdAsync(int clientId);
	Task AddAsync(Discount discount);
}