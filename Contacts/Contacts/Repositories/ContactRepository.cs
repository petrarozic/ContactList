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

        public ContactDTO GetContactById(int contactId, string userId)
        {
            var contact = _appDbContext.Contacts
                                .Where(c => c.ContactId == contactId) 
                                .Where(c => c.ApplicationUser.Id == userId)
                                .Include(p => p.PhoneNumbers)
                                .Include(i => i.ProfilePhoto)
                                .FirstOrDefault();

            if (contact == null) return null;
            ContactDTO contactDTO = _mapper.Map<ContactDTO>(contact);

            return contactDTO;
        }

        public int AddContact(ContactDTO contactDTO, byte[] content, string userId)
        {
            Contact contact = _mapper.Map<Contact>(contactDTO);
            contact.ProfilePhoto = _mapper.Map<ProfilePhoto>(content);
            contact.ApplicationUser = _mapper.Map<ApplicationUser>(userId);

            _appDbContext.Contacts.Add(contact);
            _appDbContext.SaveChanges();
            return contact.ContactId;
        }

        public List<ContactDTO> GetAllContact(int page, int contactPerPage, int sortBy, string userId, string searchByFirstName = null, string searchByLastName = null, string searchByCity = null, string searchByPhoneNumber = null)
        {
            IQueryable<Contact> contacts = _appDbContext.Contacts
                                                .Where(c => c.ApplicationUser.Id == userId)
                                                .Include(p => p.PhoneNumbers);
            contacts = FilterListOfContacts(contacts, searchByFirstName, searchByLastName, searchByCity, searchByPhoneNumber);

            if (page != -1 && contactPerPage != -1 && sortBy != -1)
            {
                switch (sortBy)
                {
                    case 1: //By first name - ASC
                        contacts = contacts.OrderBy(c => c.FirstName);
                        break;
                    case 2: //By last name - ASC
                        contacts = contacts.OrderBy(c => c.LastName);
                        break;
                    case 3: //By city name - ASC
                        contacts = contacts.OrderBy(c => c.City);
                        break;
                    case 4: //By first name - DESC
                        contacts = contacts.OrderByDescending(c => c.FirstName);
                        break;
                    case 5: //By last name - DESC
                        contacts = contacts.OrderByDescending(c => c.LastName);
                        break;
                    case 6: //By city name - DESC
                        contacts = contacts.OrderByDescending(c => c.City);
                        break;
                };

                contacts = contacts.Skip(page * contactPerPage)
                    .Take(contactPerPage);
            }

            if (contacts == null) return null;
            List<ContactDTO> contactDTOs = _mapper.Map<List<ContactDTO>>(contacts);

            return contactDTOs;
        }

        private IQueryable<Contact> FilterListOfContacts(IQueryable<Contact> contacts, string searchByFirstName, string searchByLastName, string searchByCity, string searchByPhoneNumber)
        {
            int num0 = contacts.Count();
            if (!contacts.Any()) return contacts;
            
            if (searchByFirstName != null) contacts = contacts.Where(c => c.FirstName.Contains(searchByFirstName));
            if (searchByLastName != null) contacts = contacts.Where(c => c.LastName.Contains(searchByLastName));
            if (searchByCity != null) contacts = contacts.Where(c => c.City.Contains(searchByCity));
            if (searchByPhoneNumber != null) contacts = contacts.Where(c => ContainsPhoneNumber(c.PhoneNumbers, searchByPhoneNumber));

            return contacts;
        }

        private bool ContainsPhoneNumber(ICollection<PhoneNumber> phoneNumbers, string searchByPhoneNumber)
        {
            foreach(var x in phoneNumbers)
            {
                if (x.Number.Contains(searchByPhoneNumber)) return true;
            }

            return false;
        }

        public ProfilePhoto GetProfilePhoto(int id)
        {
            return _appDbContext.ProfilePhotos
                .Where(i => i.ProfilePhotoId == id)
                .FirstOrDefault();
        }

        public bool DeleteContact(int contactId, string userId)
        {
            if (!OwnerOfContact(contactId, userId)) return false;

            try
            {
                Contact contact = _appDbContext.Contacts
                                    .Where(c => c.ContactId == contactId)
                                    .Where(c => c.ApplicationUser.Id == userId)
                                    .Include(p => p.PhoneNumbers)
                                    .Include(i => i.ProfilePhoto)
                                    .FirstOrDefault();

                foreach(var x in contact.PhoneNumbers)
                {
                    _appDbContext.Remove(x);
                }

                _appDbContext.Remove(contact);
                _appDbContext.SaveChanges();
            }
            catch (InvalidCastException e)
            {
                return false;
            }
            return true;
        }

        public bool CanEditContact(int contactId, string userId)
        {
            return OwnerOfContact(contactId, userId);
        }

        private bool OwnerOfContact(int contactId, string userId)
        {
            var foreignKey = _appDbContext.Contacts
                                .Where(c => c.ContactId == contactId)
                                .Select(c => c.ApplicationUser.Id)
                                .FirstOrDefault();

            if (foreignKey != userId) return false;
            return true;
        }

        public void EditRecipe(ContactDTO contact, byte[] content)
        {
            var currentContact = _appDbContext.Contacts
                                .Where(c => c.ContactId == contact.ContactId)
                                .Include(p => p.PhoneNumbers)
                                .Include(i => i.ProfilePhoto)
                                .FirstOrDefault();

            currentContact.FirstName = contact.FirstName; 
            currentContact.LastName= contact.LastName; 
            currentContact.Note= contact.Note;
            currentContact.City = contact.City;

            currentContact.PhoneNumbers.Clear();
            foreach(var x in contact.PhoneNumbers)
            {
                currentContact.PhoneNumbers.Add(
                        new PhoneNumber
                        {
                            Type = x.Type,
                            Number = x.Number,
                            Note = x.Note
                        }
                    );
            }

            if (content != null)
                if (currentContact.ProfilePhoto != null) currentContact.ProfilePhoto.Content = content;
                else currentContact.ProfilePhoto = new ProfilePhoto { Content = content };

            _appDbContext.SaveChanges();
        }
    }
}
