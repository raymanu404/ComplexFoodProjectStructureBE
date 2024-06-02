using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrdersStatistics;
public class Request
{
    public DateTime startDate { get; set; } = new DateTime(2024, 1, 1);
    public DateTime endDate { get; set; } = DateTime.Now;
}
