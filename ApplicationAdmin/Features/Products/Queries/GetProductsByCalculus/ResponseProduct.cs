using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAdmin.Features.Products.Queries.GetProductsByCalculus;
public class ResponseProduct
{
    public string CategoryName { get; set; }
    public int TotalProducts { get; set; }
    public int InStock { get; set; }
    public int OutOfStock { get; set; }
    public double TotalPrice { get; set; }
    public double TotalSellingPrice { get; set; }
    public double TotalProfit { get; set; }
    public double TotalProfitWithoutVTA { get; set; }
    public double TotalProfitWithVTA { get; set; }
}
