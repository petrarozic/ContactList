using Contacts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.ViewModels
{
    public class HomeViewModel
    {
        public List<ContactDTO> Contacts { get; set; }

        public int CurrentPage { get; set; }
        public int MinPage { get; set; }
        public int MaxPage { get; set; }

        public int ContactsPerPage { get; set; }
    }
}
