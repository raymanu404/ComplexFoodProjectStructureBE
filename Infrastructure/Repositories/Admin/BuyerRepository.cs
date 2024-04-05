using Application.Contracts.Persistence.Admin;
using Domain.Models.Roles;
using Microsoft.EntityFrameworkCore;
using Domain.ValueObjects;

namespace Infrastructure.Repositories.Admin;

public class BuyerRepository : IBuyerRepository
{
    private readonly ApplicationContext _context;

    public BuyerRepository(ApplicationContext context)
    {
        _context = context;
    }

    public void Delete(Buyer buyer) => _context.Buyers.Remove(buyer);

    public async Task<Buyer?> GetByIdAsync(int id)
    {

        var buyer = await _context.Buyers.Where(x => x.Id == id).FirstOrDefaultAsync();
        return buyer;
    }

    public async Task<List<Buyer>> GetAllAsync()
    {
        var query = _context.Buyers
            .Include(x => x.Coupons)
            .Select(x => new
            {
                x.Id,
                x.Email,
                x.FirstName,
                x.LastName,
                x.Password,
                x.PhoneNumber,
                x.Gender,
                x.Confirmed,
                x.Balance,
                //x.Coupons,
            });
        var buyers = await query.ToListAsync().ConfigureAwait(false);
        return buyers.Select(x => new Buyer()
        {
            Id = x.Id,
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Password = x.Password,
            PhoneNumber = x.PhoneNumber,
            Gender = x.Gender,
            Confirmed = x.Confirmed,
            Balance = x.Balance,
            //Coupons = x.Coupons
        }).ToList();
    }

    public async Task<bool> GetBuyerByEmail(Email email)
    {
        var query = from buyer in _context.Buyers
                    where buyer.Email == email
                    select buyer.Email;
        var result = await query.FirstOrDefaultAsync();
        return result.Value != null;
    }

    public async Task<Buyer?> GetBuyerByEmailAsync(Email email)
    {
        var buyer = await _context.Buyers.Where(x => x.Email == email).FirstOrDefaultAsync();
        return buyer;
    }
}