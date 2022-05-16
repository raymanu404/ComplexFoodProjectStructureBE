using Application.Contracts.Persistence;
using Domain.Models.Roles;
using Microsoft.EntityFrameworkCore;
using Application.DtoModels.Buyer;
using AutoMapper;
using Domain.ValueObjects;

namespace Infrastructure.Repositories;

public class BuyerRepository : IBuyerRepository
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;
        
    public BuyerRepository(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddAsync(Buyer buyer) => await _context.Buyers.AddAsync(buyer);

    public void Update(BuyerDto buyer)
    {

        //nu merge cu metoda asta
        var buyerToUpdate = _mapper.Map<Buyer>(buyer);
        _context.Buyers.Update(buyerToUpdate);
    }

    public void Delete(Buyer buyer) =>  _context.Buyers.Remove(buyer); 

    public async Task<Buyer?> GetByIdAsync(int id) => await _context.Buyers.FindAsync(id);

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
                x.Coupons,
            });
        var buyers = await query.ToListAsync().ConfigureAwait(false);
        return buyers.Select(x => new Buyer() { 
            Id = x.Id,
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Password = x.Password,
            PhoneNumber = x.PhoneNumber,
            Gender = x.Gender,
            Confirmed = x.Confirmed,
            Balance = x.Balance,
            Coupons = x.Coupons
        }).ToList();
    }

    public async Task<BuyerDto?> LoginBuyer(Email email, Password password)
    {
        var buyer = await _context.Buyers.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        return _mapper.Map<BuyerDto?>(buyer);
    }

}