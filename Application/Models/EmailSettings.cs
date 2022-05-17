using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class EmailSettings
    {
        public string Password { get; set; } = null!;
        public string Sender { get; set; } = null!;
        public string Subject { get; set; } = null!;
    }
}
