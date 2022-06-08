using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Models.Roles;
using MediatR;
using Application.DtoModels.Buyer;
using Application.Components;
using Application.Components.RandomCode;
using Application.Contracts.FileUtils;
using Domain.ValueObjects;
using Application.Models;
using Microsoft.Extensions.Options;

namespace Application.Features.Buyers.Commands.CreateBuyer;

public class CreateBuyerCommandHandler : IRequestHandler<CreateBuyerCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    //private readonly IFileReader _fileReader;
    private readonly EmailSettings _emailSettings;
    public CreateBuyerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IOptions<EmailSettings> emailSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        //_fileReader = fileReader;
        _emailSettings = emailSettings.Value;
    }

    public async Task<string> Handle(CreateBuyerCommand command, CancellationToken cancellationToken)
    {
        var returnMessage = "";
        var checkBuyerIfExists = await _unitOfWork.Buyers.GetBuyerByEmail(new Email(command.Buyer.Email));
        if (!checkBuyerIfExists)
        {

            if (command.Buyer.Email.EndsWith("email.com"))
            {
                return "Email invalid!";
            }

            //verificare inainte de hash
            var checkNewPassword = new Password(command.Buyer.Password);
            if (checkNewPassword.Value.Equals(""))
            {
                return "Password Invalid!";
            }

            //aici ar trebui sa criptam parolele
            command.Buyer.Password = EncodePassword.ComputeSha256Hash(command.Buyer.Password);
            var buyer = _mapper.Map<Buyer>(command.Buyer);

            if (buyer.Email.Value.Equals(""))
            {
                return "Email invalid!";
            }

            if (buyer.FirstName.Value.Equals(""))
            {
                return "FirstName invalid!";
            }

            if (buyer.LastName.Value.Equals(""))
            {
                return "LastName invalid!";
            } 
            
            if (buyer.PhoneNumber.Value.Equals(""))
            {
                return "PhoneNumber invalid!";
            }

           
            if (buyer.Gender.Value.Equals(""))
            {
                return "Gender invalid!";
            }

            buyer.ConfirmationCode = new UniqueCode(RandomCode.GetRandomCode(6));
            await _unitOfWork.Buyers.AddAsync(buyer);
            await _unitOfWork.CommitAsync(cancellationToken);

            //send mail
            string mailFrom = _emailSettings.Sender;
            string subject = _emailSettings.Subject;
            string body = $"Codul dumneavoastra : {buyer.ConfirmationCode.Value}";
            string nameTo = buyer.FirstName.Value + " " + buyer.LastName.Value;

            var mailStatus = StmpGmail.SendMail(mailFrom, _emailSettings.Password, buyer.Email.Value, subject, body, nameTo);
            if (mailStatus.Equals("OK"))
            {
                returnMessage = $"{buyer.Email.Value}";
            }
        }
        else
        {
            return "This account exists/invalid!";
        }

        return returnMessage;
    }

   


}