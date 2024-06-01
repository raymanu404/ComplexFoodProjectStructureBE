using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationAdmin.Contracts.Abstractions;

namespace ApplicationAdmin.Features.Products.Queries.GetProductsByCalculus;

public class Response
{

    public ResponseData<ResponseCalculus> CalculusData { get; set; }
    public int TotalProducts { get; set; }
    public int TotalInStock { get; set; }
    public int TotalOutOfStock { get; set; }
    public double TotalPrice { get; set; }
    public double TotalMerchantPrice { get; set; }
    public double TotalProfitWithoutVTA { get; set; }
    public double TotalProfitWithVTA { get; set; }


    public class ResponseCalculus
    {
        public string CategoryName { get; set; }
        public int TotalProducts { get; set; }
        public int InStock { get; set; }
        public int OutOfStock { get; set; }
        public double TotalPrice { get; set; }
        public double TotalMerchantPrice { get; set; }
        public double TotalProfitWithoutVTA { get; set; }
        public double TotalProfitWithVTA { get; set; }
    }
}
