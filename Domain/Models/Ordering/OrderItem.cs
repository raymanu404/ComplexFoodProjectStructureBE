using Domain.ValueObjects;
using Domain.Models.Enums;
using Domain.Models.Shopping;

namespace Domain.Models.Ordering;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public Cantity Cantity { get; set; }
    public Categories Category { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Image { get; set; } = null!;
    public Price Price { get; set; }
    public int OrderId { get; set; }

    //reference only one to one
    public Product Product { get; set; } = null!;
    public int ProductId { get; set; }

}