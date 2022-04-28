using Application.Contracts.Persistence;
using Domain.ValueObjects;
using MediatR;
using AutoMapper;
using Domain.Models.Roles;
using Application.DtoModels.Buyer;

namespace Application.Features.Buyers.Commands.UpdateBuyer;

public class UpdateBuyerCommandHandler : IRequestHandler<UpdateBuyerCommand, BuyerDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateBuyerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BuyerDto> Handle(UpdateBuyerCommand command, CancellationToken cancellationToken)
    {
        var buyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);

        
        if (buyer != null)
        {
            //buyer.Email = command.Buyer.Email.Value != null ? command.Buyer.Email : buyer.Email;
            //buyer.FirstName = command.Buyer.FirstName.Value != null ? command.Buyer.FirstName : buyer.FirstName;
            //buyer.LastName = command.Buyer.LastName.Value != null ? command.Buyer.LastName : buyer.LastName;
            //buyer.Gender = command.Buyer.Gender.Value != null ? command.Buyer.Gender : buyer.Gender;
            //buyer.PhoneNumber = command.Buyer.PhoneNumber.Value != null ? command.Buyer.PhoneNumber : buyer.PhoneNumber;

            //_unitOfWork.Buyers.Update(command.Buyer);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        return command.Buyer;
    }
}