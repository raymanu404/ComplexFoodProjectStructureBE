using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrdersStatistics;
public class Response
{
    public int TotalOrders { get; set; }
    public double TotalPrice { get; set; }
    public double TotalMerchantPrice { get; set; }
    public double TotalProfitWithoutVTA { get; set; }
    public double TotalProfitWithVTA { get; set; }

}
