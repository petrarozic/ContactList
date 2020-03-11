using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.DTO
{
    public class ContactDTO
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Note { get; set; }

        public List<PhoneNumberDTO> PhoneNumbers { get; set; }
    }
}
