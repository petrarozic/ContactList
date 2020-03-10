using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Note { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
}
}
