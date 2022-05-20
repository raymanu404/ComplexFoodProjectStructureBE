using Application.Contracts.Persistence;
using AutoMapper;
using Domain.Models.Roles;
using MediatR;
using Application.DtoModels.Buyer;
using Application.Components;
using Application.Components.RandomCode;
using Application.Contracts.FileUtils;
using Domain.ValueObjects;

namespace Application.Features.Buyers.Commands.CreateBuyer;

public class CreateBuyerCommandHandler : IRequestHandler<CreateBuyerCommand, BuyerRegisterDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileReader _fileReader;

    public CreateBuyerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileReader fileReader)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileReader = fileReader;
    }

    public async Task<BuyerRegisterDto> Handle(CreateBuyerCommand command, CancellationToken cancellationToken)
    {
        if (command.Buyer.Email.EndsWith("email.com"))
        {
            return null;
        }

        //aici ar trebui sa criptam parolele
        command.Buyer.Password =  EncodePassword.ComputeSha256Hash(command.Buyer.Password);    
        var buyer = _mapper.Map<Buyer>(command.Buyer);

        buyer.ConfirmationCode = new UniqueCode(RandomCode.GetRandomCode(6));
        await _unitOfWork.Buyers.AddAsync(buyer);
        await _unitOfWork.CommitAsync(cancellationToken);

        //readFromFile
        string currentDir = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
        string[] files = Directory.GetFiles(currentDir, "*.txt");
        string currentfile = Path.Combine(currentDir, files[0]);
        var result = _fileReader.ReadFile(currentfile);
        var finalResultDecrypted = SecureData.Decrypt(result);

        //send mail
        string mailFrom = "caprariuemanuel58@gmail.com";
        string subject = "Confirmare cont ComplexFoodApp";
        string body = $"Codul dumneavoastra : {buyer.ConfirmationCode.Value}";
        string nameTo = buyer.FirstName.Value + " " + buyer.LastName.Value;

        var mailStatus = StmpGmail.SendMail(mailFrom, finalResultDecrypted, buyer.Email.Value, subject, body, nameTo);
        if (mailStatus.Equals("OK"))
        {
            Console.WriteLine("A fost trimis mailul");
        }

        return command.Buyer;
    }

   


}