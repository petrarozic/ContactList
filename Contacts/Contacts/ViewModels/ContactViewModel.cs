using Contacts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.ViewModels
{
    public class ContactViewModel
    {
        public ContactDTO Contact { get; set; }
        public string ErrorMessage { get; set; }
    }
}
