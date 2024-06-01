using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAdmin.Features.Products.Queries.GetProductsByCalculus;
public class ProductCalculusFromBody
{
    public DateTime startDate { get; set; } = DateTime.Now.Date;
    public DateTime endDate { get; set; } = DateTime.Now;
}
