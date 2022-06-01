using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DtoModels.PaymentDto
{
    public class PaymentIntentDto
    {
        public string Email { get; set; }
        public long Amount { get; set; }
    }
}
