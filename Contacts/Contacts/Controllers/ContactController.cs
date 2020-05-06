using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contacts.DTO;
using Contacts.Interfaces;
using Contacts.Models;
using Contacts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactController(IContactRepository contactRepository, UserManager<ApplicationUser> userManager)
        {
            _contactRepository = contactRepository;
            _userManager = userManager;
        }

        [Route("Contact/{contactId:int}")]
        public async Task<IActionResult> Index(int contactId)
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            ContactViewModel contactViewModel = new ContactViewModel
            {
                Contact = _contactRepository.GetContactById(contactId, applicationUser.Id)
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
        public async Task<IActionResult> AddContact(ContactViewModel contactViewModel, IFormFile file)
        {
           byte[] content = null;
           if(file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        content = memoryStream.ToArray();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                }
            }

            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            int contactId = _contactRepository.AddContact(contactViewModel.Contact, content, applicationUser.Id);
            return RedirectToAction("Index", "Contact", new { contactId = contactId });
        }

        public IActionResult ShowProfilePhoto(int id)
        {
            ProfilePhoto image = _contactRepository.GetProfilePhoto(id);
            if (image == null)
            {
                byte[] imageContent = System.IO.File.ReadAllBytes("wwwroot\\images\\profile-icon.png");
                return File(imageContent, "image/jpg");
            }
            return File(image.Content, "image/jpg");
        }

        [Route("Contact/DeleteContact/{contactId}")]
        public async Task<IActionResult> DeleteContact (int contactId)
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            bool deleteSuccessed = _contactRepository.DeleteContact(contactId, applicationUser.Id);
            if (!deleteSuccessed)
            {
                ViewBag.Error = "You do not have permission to delete this contact";
                return View("Warning");
            }
            return RedirectToAction("Index", "Home");
        }

        [Route("Contact/EditContact/{contactId}")]
        public async Task<IActionResult> EditContact(int contactId)
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            bool canEditContact = _contactRepository.CanEditContact(contactId, applicationUser.Id);

            if (!canEditContact)
            {
                ViewBag.Error = "You do not have permission to edit this contact";
                return View("Warning");
            }

            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.Contact = _contactRepository.GetContactById(contactId, applicationUser.Id);
            
            return View(contactViewModel);
        }

        [HttpPost]
        [Route("Contact/EditContact/{contactId}")]
        public async Task<IActionResult> EditRecipe(ContactViewModel contactViewModel, IFormFile file)
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            bool canEditContact = _contactRepository.CanEditContact(contactViewModel.Contact.ContactId, applicationUser.Id);

            if (!canEditContact)
            {
                ViewBag.Error = "You do not have permission to edit this contact";
                return View("Warning");
            }

            byte[] content = null;
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        content = memoryStream.ToArray();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                }
            }

            _contactRepository.EditRecipe(contactViewModel.Contact, content);

            return RedirectToAction("Index", "Contact", new { recipeId = contactViewModel.Contact.ContactId });
        }
    }
}