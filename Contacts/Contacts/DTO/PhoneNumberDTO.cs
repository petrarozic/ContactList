using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.DTO
{
    public class PhoneNumberDTO
    {
        public int PhoneNumberId { get; set; }

        public string Type { get; set; }
        public string Number { get; set; }
        public string Note { get; set; }

        public int ContactId { get; set; }
    }
}
