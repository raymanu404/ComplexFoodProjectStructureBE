using Domain.Models.Enums;


namespace ApplicationAdmin.DtoModels.Product;
public class ProductUpdateDto
{
    public Categories? Category { get; set; }
    public string? Title { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public double? Price { get; set; }
    public string? Image { get; set; } = null!;
    public bool? IsInStock { get; set; }
}
