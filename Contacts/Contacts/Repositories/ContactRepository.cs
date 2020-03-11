using Contacts.DTO;
using Contacts.Interfaces;
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

        public ContactRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<ContactDTO> GetAllContact()
        {
            var contacts = _appDbContext.Contacts
                                .Include(p => p.PhoneNumbers);

            if (contacts == null) return null;

            List<ContactDTO> contactDTOs = new List<ContactDTO>();
            foreach(var c in contacts)
            {
                ContactDTO contactDTO = new ContactDTO
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    City = c.City,
                    ContactId = c.ContactId,
                    Note = c.Note
                };

                List<PhoneNumberDTO> phoneNumberDTOs = new List<PhoneNumberDTO>();
                foreach(var p in c.PhoneNumbers)
                {
                    PhoneNumberDTO phoneNumberDTO = new PhoneNumberDTO
                    {
                        ContactId = c.ContactId,
                        PhoneNumberId = p.PhoneNumberId,
                        Type = p.Type,
                        Number = p.Number,
                        Note = p.Note
                    };
                    phoneNumberDTOs.Add(phoneNumberDTO);
                }
                contactDTO.PhoneNumbers = phoneNumberDTOs;
                contactDTOs.Add(contactDTO);
            }
            return contactDTOs;
        }
    }
}
