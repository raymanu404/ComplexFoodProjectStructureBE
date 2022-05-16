using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Models.Roles;
using MediatR;
using Application.DtoModels.Buyer;
using Application.Components;
using Application.Components.RandomCode;

namespace Application.Features.Buyers.Commands.CreateBuyer;

public class CreateBuyerCommandHandler : IRequestHandler<CreateBuyerCommand, BuyerDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBuyerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BuyerDto> Handle(CreateBuyerCommand command, CancellationToken cancellationToken)
    {
        //aici ar trebui sa criptam parolele
        command.Buyer.Password =  EncodePassword.ComputeSha256Hash(command.Buyer.Password);    
        var buyer = _mapper.Map<Buyer>(command.Buyer);

        buyer.ConfirmationCode = new Domain.ValueObjects.UniqueCode(RandomCode.GetRandomCode(6));
        await _unitOfWork.Buyers.AddAsync(buyer);
        await _unitOfWork.CommitAsync(cancellationToken);

        //readFromFile
        string currentDir = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
        string[] files = Directory.GetFiles(currentDir, "*.txt");
        string currentfile = Path.Combine(currentDir, files[0]);
        var result = currentfile.ReadFile();
        var finalResultDecrypted = SecureData.Decrypt(result);

        //send mail
        string mailFrom = "caprariuemanuel58@gmail.com";
        string subject = "Confirmare cont ComplexFoodApp";
        string body = $"Codul dumneavoastra : {buyer.ConfirmationCode.Value}";
        string nameTo = buyer.FirstName.Value  + " " + buyer.LastName.Value;

        var mailStatus = StmpGmail.SendMail(mailFrom, finalResultDecrypted, buyer.Email.Value, subject, body, nameTo);
        if (mailStatus.Equals("OK"))
        {
            Console.WriteLine("A fost trimis mailul");
        }

        return command.Buyer;
    }

   


}