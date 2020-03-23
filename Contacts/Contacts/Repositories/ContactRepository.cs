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

        public int AddContact(ContactDTO contactDTO)
        {
            Contact contact = _mapper.Map<Contact>(contactDTO);
            _appDbContext.Contacts.Add(contact);
            _appDbContext.SaveChanges();
            return contact.ContactId;
        }

        public List<ContactDTO> GetAllContact(int page, int contactPerPage, int sortBy)
        {
            IQueryable<Contact> contacts = null;
            switch (sortBy)
            {
                case 1: //By first name - ASC
                    contacts = _appDbContext.Contacts
                                .Include(p => p.PhoneNumbers)
                                .OrderBy(c => c.FirstName)
                                .Skip(page * contactPerPage)
                                .Take(contactPerPage);
                    break;
                case 2: //By last name - ASC
                    contacts = _appDbContext.Contacts
                                .Include(p => p.PhoneNumbers)
                                .OrderBy(c => c.LastName)
                                .Skip(page * contactPerPage)
                                .Take(contactPerPage);
                    break;
                case 3: //By city name - ASC
                    contacts = _appDbContext.Contacts
                                .Include(p => p.PhoneNumbers)
                                .OrderBy(c => c.City)
                                .Skip(page * contactPerPage)
                                .Take(contactPerPage);
                    break;
                case 4: //By first name - DESC
                    contacts = _appDbContext.Contacts
                                .Include(p => p.PhoneNumbers)
                                .OrderByDescending(c => c.FirstName)
                                .Skip(page * contactPerPage)
                                .Take(contactPerPage);
                    break;
                case 5: //By last name - DESC
                    contacts = _appDbContext.Contacts
                                .Include(p => p.PhoneNumbers)
                                .OrderByDescending(c => c.LastName)
                                .Skip(page * contactPerPage)
                                .Take(contactPerPage);
                    break;
                case 6: //By city name - DESC
                    contacts = _appDbContext.Contacts
                                .Include(p => p.PhoneNumbers)
                                .OrderByDescending(c => c.City)
                                .Skip(page * contactPerPage)
                                .Take(contactPerPage);
                    break;
            };

            if (contacts == null) return null;
            List<ContactDTO> contactDTOs = _mapper.Map<List<ContactDTO>>(contacts);

            return contactDTOs;
        }
    }
}
