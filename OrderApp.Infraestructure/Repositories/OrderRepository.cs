using Microsoft.EntityFrameworkCore;
using OrderApp.Domain.Entities;
using OrderApp.Domain.Interfaces;
using OrderApp.Infraestructure.Data;

namespace OrderApp.Infraestructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Order>> GetAllAsync() =>  
        await _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .ToListAsync();
    public async Task<Order?> GetByIdAsync(int id)  
        =>  await _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .FirstOrDefaultAsync(e => e.Id == id);
    
    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task UpdateAsync(Order order)
    {
       _context.Orders.Update(order);
         await _context.SaveChangesAsync();
      
    }

    public async Task DeleteAsync(int id)
    {
      var order = await GetByIdAsync(id);
      if (order != null)
      {
          _context.Orders.Remove(order);
          await _context.SaveChangesAsync();
      }
    }
}