using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.DTO
{
    public class ContactDTO
    {
        public int ContactId { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        public string Note { get; set; }

        public List<PhoneNumberDTO> PhoneNumbers { get; set; }
        [Display(Name = "Phone numbers")]
        public string PhoneNumbersString { get; set; }
    }
}
