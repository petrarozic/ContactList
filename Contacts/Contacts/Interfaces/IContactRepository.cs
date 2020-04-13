using Contacts.DTO;
using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Interfaces
{
    public interface IContactRepository
    {
        List<ContactDTO> GetAllContact(int page, int contactPerPage, int sortBy, string userId, string searchByFirstName = null, string searchByLastName = null, string searchByCity = null, string searchByPhoneNumber = null);
        ContactDTO GetContactById(int contactId, string userId);

        int AddContact(ContactDTO contactDTO, byte[] content, string userId);

        ProfilePhoto GetProfilePhoto(int id);
    }
}
