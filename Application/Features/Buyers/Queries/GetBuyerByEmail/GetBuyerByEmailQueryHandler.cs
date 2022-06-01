using MediatR;
using Application.Contracts.Persistence;
using Application.DtoModels.Buyer;
using Application.Components;
using Application.Models;
using Microsoft.Extensions.Options;
using Domain.ValueObjects;
using Application.Components.RandomCode;

namespace Application.Features.Buyers.Queries.GetBuyerByEmail
{
    public class GetBuyerByEmailQueryHandler : IRequestHandler<GetBuyerByEmailQuery, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSettings _emailSettings;
        public GetBuyerByEmailQueryHandler(IUnitOfWork unitOfWork, IOptions<EmailSettings> emailSettings)
        {
            _unitOfWork = unitOfWork;
            _emailSettings = emailSettings.Value;
        }
        public async Task<string> Handle(GetBuyerByEmailQuery request, CancellationToken cancellationToken)
        {
            string returnMessage = "";

            try
            {
                var checkBuyerIfExists = await _unitOfWork.Buyers.GetBuyerByEmailAsync(new Email(request.EmailBuyer.Email));
                if(checkBuyerIfExists != null)
                {

                    checkBuyerIfExists.ConfirmationCode = new UniqueCode(RandomCode.GetRandomCode(6));
                    await _unitOfWork.CommitAsync(cancellationToken);

                    //send mail
                    string mailFrom = _emailSettings.Sender;
                    string subject = _emailSettings.Subject;
                    string body = $"Codul dumneavoastra : {checkBuyerIfExists.ConfirmationCode.Value} pentru resetare parola!";
                    string nameTo = checkBuyerIfExists.FirstName.Value + " " + checkBuyerIfExists.LastName.Value;

                    var mailStatus = StmpGmail.SendMail(mailFrom, _emailSettings.Password, request.EmailBuyer.Email, subject, body, nameTo);
                    if (mailStatus.Equals("OK"))
                    {
                        returnMessage = $"A fost trimis mailul!{checkBuyerIfExists.Id}";
                    }
                }
                else
                {
                    return "Emailul nu exista!";
                }
               
            }
            catch(Exception ex)
            {
                returnMessage = $"Eroare: {ex.Message}";
            }

            return returnMessage;
        }
    }
}
