using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAdmin.Features.Orders.Queries.GetOrdersStatistics;
public class Response
{
    public DataInPercents DataInPercentsResponse { get; set; } = null!;
    public DataInPeriodOfTime DataInPeriodOfTimeResponse { get; set; } = null!;
    public TotalData TotalDataResponse { get; set; } = null!;

    public class DataInPercents
    {
        public double TotalOrders { get; set; }
        public double TotalPrice { get; set; }
        public double TotalMerchantPrice { get; set; }
        public double TotalProfitWithoutVTA { get; set; }
        public double TotalProfitWithVTA { get; set; }
    }

    public class DataInPeriodOfTime
    {
        public int TotalOrders { get; set; }
        public double TotalPrice { get; set; }
        public double TotalMerchantPrice { get; set; }
        public double TotalProfitWithoutVTA { get; set; }
        public double TotalProfitWithVTA { get; set; }
    }
    public class TotalData
    {
        public int TotalOrders { get; set; }
        public double TotalPrice { get; set; }
        public double TotalMerchantPrice { get; set; }
        public double TotalProfitWithoutVTA { get; set; }
        public double TotalProfitWithVTA { get; set; }
    }
}
