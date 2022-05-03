using Application.Contracts.Persistence;
using Domain.ValueObjects;
using MediatR;
using AutoMapper;
using Domain.Models.Roles;
using Application.DtoModels.Buyer;

namespace Application.Features.Buyers.Commands.UpdateBuyer;

public class UpdateBuyerCommandHandler : IRequestHandler<UpdateBuyerCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateBuyerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateBuyerCommand command, CancellationToken cancellationToken)
    {
        var buyer = await _unitOfWork.Buyers.GetByIdAsync(command.BuyerId);
    
        if (buyer != null)
        {

            if (command.Buyer.Email == null || command.Buyer.Email.Equals("string"))
            {
                command.Buyer.Email = buyer.Email.Value;
            }

            if (command.Buyer.FirstName == null || command.Buyer.FirstName.Equals("string"))
            {
                command.Buyer.FirstName = buyer.FirstName.Value;
            }

            if (command.Buyer.LastName == null || command.Buyer.LastName.Equals("string"))
            {
                command.Buyer.LastName = buyer.LastName.Value;
            }

            if (command.Buyer.PhoneNumber == null || command.Buyer.PhoneNumber.Equals("string"))
            {
                command.Buyer.PhoneNumber = buyer.PhoneNumber.Value;
            }

            if (command.Buyer.Gender == null || command.Buyer.Gender.Equals("string"))
            {
                command.Buyer.Gender = buyer.Gender.Value;
            }

            buyer.Email = new Email(command.Buyer.Email);
            buyer.FirstName = new Name(command.Buyer.FirstName);
            buyer.LastName =new Name(command.Buyer.LastName);
            buyer.Gender = new Gender(command.Buyer.Gender);
            buyer.PhoneNumber =new PhoneNumber(command.Buyer.PhoneNumber);

            //buyer. = _mapper.Map<Buyer>(command.Buyer);

            //_unitOfWork.Buyers.Update(command.Buyer);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        return Unit.Value;
    }
}