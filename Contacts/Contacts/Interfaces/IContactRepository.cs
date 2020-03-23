using Contacts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Interfaces
{
    public interface IContactRepository
    {
        List<ContactDTO> GetAllContact(int page, int contactPerPage, int sortBy, string searchByFirstName = null, string searchByLastName = null, string searchByCity = null, string searchByPhoneNumber = null);
        ContactDTO GetContactById(int contactId);

        int AddContact(ContactDTO contactDTO);
    }
}
