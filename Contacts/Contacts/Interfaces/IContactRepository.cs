using Contacts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Interfaces
{
    public interface IContactRepository
    {
        List<ContactDTO> GetAllContact();
        ContactDTO GetContactById(int contactId);
    }
}
