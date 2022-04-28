using MediatR;
using Application.Contracts.Persistence;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if(productToDelete != null)
            {             
                _unitOfWork.Products.Delete(productToDelete);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
