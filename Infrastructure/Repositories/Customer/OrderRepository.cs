﻿using Application.Contracts.Persistence;
using Domain.Models.Ordering;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Customer
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);


        public void DeleteOrder(Order order) => _context.Orders.Remove(order);

        public async Task<List<Order>> GetAllAsync() => await _context.Orders.ToListAsync();

        public async Task<List<Order>> GetAllOrdersByBuyerId(int buyerId) => await _context.Orders.Where(x => x.BuyerId == buyerId).OrderBy(x => x.Id).ToListAsync();
        public async Task<Order?> GetByIdAsync(int id) => await _context.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Order?> GetOrderByBuyerId(int buyerId) => await _context.Orders.Where(x => x.BuyerId == buyerId).OrderBy(x => x.Id).LastOrDefaultAsync();


    }
}
