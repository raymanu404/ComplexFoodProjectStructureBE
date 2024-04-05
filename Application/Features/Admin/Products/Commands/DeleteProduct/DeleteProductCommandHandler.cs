using Application.Contracts.Persistence.Admin;
using MediatR;

namespace Application.Features.Admin.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IUnitOfWorkAdmin _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWorkAdmin unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if (productToDelete != null)
            {
                _unitOfWork.Products.Delete(productToDelete);
                await _unitOfWork.CommitAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
