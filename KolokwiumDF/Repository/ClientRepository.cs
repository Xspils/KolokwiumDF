using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KolokwiumDF.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly baza _context;

    public ClientRepository(baza context)
    {
        _context = context;
    }

    public async Task<Client> GetByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task AddAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}
