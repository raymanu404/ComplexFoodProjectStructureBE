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
        
    public BuyerRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Buyer buyer) => await _context.Buyers.AddAsync(buyer);
    public void Delete(Buyer buyer) =>  _context.Buyers.Remove(buyer);

    public async Task<Buyer?> GetByIdAsync(int id)
    {
        //var query = _context.Buyers
        //    .Include(x => x.Coupons)
        //    .Select(x => new
        //    {
        //        x.Id,
        //        x.Email,
        //        x.FirstName,
        //        x.LastName,
        //        x.Password,
        //        x.PhoneNumber,
        //        x.Gender,
        //        x.Confirmed,
        //        x.Balance,
        //        x.ConfirmationCode,
        //        x.Coupons,

        //    }).Where(x => x.Id == id).FirstOrDefaultAsync();
        var buyer = await _context.Buyers.Where(x => x.Id == id).FirstOrDefaultAsync();
        return buyer;
        //var buyer = await query;
        //if(buyer == null)
        //{
        //    return new Buyer() { };
        //}

        //return new Buyer
        //{
        //    Id = buyer.Id,
        //    Email = buyer.Email,
        //    FirstName = buyer.FirstName,
        //    LastName = buyer.LastName,
        //    Password = buyer.Password,
        //    PhoneNumber = buyer.PhoneNumber,
        //    Gender = buyer.Gender,
        //    Confirmed = buyer.Confirmed,
        //    Balance = buyer.Balance,
        //    ConfirmationCode = buyer.ConfirmationCode,
        //    Coupons = buyer.Coupons,
        //};
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
            //Coupons = x.Coupons
        }).ToList();
    }

    public async Task<Buyer?> LoginBuyer(Email email, Password password)
    {
        var buyer = await _context.Buyers.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        return buyer;
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