using Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAdmin.DtoModels.Product;
public class ProductCreateDto
{
    public Categories Category { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Price { get; set; }
    public string Image { get; set; } = null!;
    public bool IsInStock { get; set; }
    public double SellingPrice { get; set; }
}
