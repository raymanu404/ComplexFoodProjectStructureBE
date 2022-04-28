using Domain.ValueObjects;
using Domain.Models.Enums;

namespace Domain.Models.Ordering;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public Amount Amount { get; set; }
    public Categories Category { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Price Price { get; set; }

}