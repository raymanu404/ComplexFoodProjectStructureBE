using ApplicationAdmin.Contracts.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationAdmin.DtoModels.Product;

namespace ApplicationAdmin.Features.Products.Queries.GetProductsByCalculus;
public class GetProductsByCalculusQuery : IRequest<Response>
{
    public DateTime startDate { get; set; } = DateTime.Now; 
    public DateTime endDate { get; set; } = DateTime.Now; 
}
