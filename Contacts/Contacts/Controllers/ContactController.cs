using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Interfaces;
using Contacts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [Route("Contact/{contactId:int}")]
        public IActionResult Index(int contactId)
        {
            ContactViewModel contactViewModel = new ContactViewModel
            {
                Contact = _contactRepository.GetContactById(contactId)
            };

            if(contactViewModel.Contact == null)
            {
                contactViewModel.ErrorMessage = "Cannot display contact."; 
            }

            return View(contactViewModel);
        }

        public IActionResult NewContact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddContact(ContactViewModel contactViewModel)
        {
            int contactId = _contactRepository.AddContact(contactViewModel.Contact);
            return RedirectToAction("Index", "Contact", new { contactId = contactId });
        }
    }
}