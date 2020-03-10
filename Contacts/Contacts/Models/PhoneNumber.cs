using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Models
{
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }

        public string Type { get; set; }
        public string Number { get; set; }
        public string Note { get; set; }

        public Contact Contact { get; set; }
    }
}
