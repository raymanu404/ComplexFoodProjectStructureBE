﻿using Domain.ValueObjects;
using Application.DtoModels.Cart;

namespace Application.DtoModels.OrderItem
{
    public class OrderItemDto
    {
        public int Cantity { get; set; }
        public int ProductId { get; set; }
    }
}
