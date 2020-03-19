using AutoMapper;
using Contacts.DTO;
using Contacts.Interfaces;
using Contacts.Models;
using Contacts.Models.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ContactRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public List<ContactDTO> GetAllContact()
        {
            var contacts = _appDbContext.Contacts
                                .Include(p => p.PhoneNumbers);
            if (contacts == null) return null;
            List<ContactDTO> contactDTOs = _mapper.Map<List<ContactDTO>>(contacts);
            
            return contactDTOs;
        }

        public ContactDTO GetContactById(int contactId)
        {
            var contact = _appDbContext.Contacts
                                .Where(c => c.ContactId == contactId)
                                .Include(p => p.PhoneNumbers)
                                .FirstOrDefault();
            if (contact == null) return null;
            ContactDTO contactDTO = _mapper.Map<ContactDTO>(contact);

            return contactDTO;
        }
    }
}
