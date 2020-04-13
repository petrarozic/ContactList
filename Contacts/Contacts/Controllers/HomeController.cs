using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Interfaces;
using Contacts.Models;
using Contacts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IContactRepository contactRepository, UserManager<ApplicationUser> userManager)
        {
            _contactRepository = contactRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            HomeViewModel homeViewModel = new HomeViewModel
            {
                Contacts = _contactRepository.GetAllContact(0, 5, 1, applicationUser.Id),

                ContactsPerPage = 5, 
                CurrentPage = 0, 
                MinPage = 0
            };

            if(homeViewModel.Contacts == null)
            {
                throw new Exception("Invalid data");
            }

            int totalNumPage = (int)Math.Ceiling((decimal)_contactRepository.GetAllContact(-1, -1, -1, applicationUser.Id).ToList().Count / (decimal)5) -1;//Page numering start with 0
            homeViewModel.MaxPage = totalNumPage < 4 ? totalNumPage : 4;

            return View(homeViewModel);
        }

        [HttpGet]
        public async Task<JsonResult> Filter (int goToPage, int recordsPerPage, int sortBy, string searchByFirstName, string searchByLastName, string searchByCity, string searchByPhoneNumber)
        {
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);

            HomeViewModel homeViewModel = new HomeViewModel()
            {
                CurrentPage = goToPage,
                ContactsPerPage = recordsPerPage,

                Contacts = _contactRepository.GetAllContact(goToPage, recordsPerPage, sortBy, applicationUser.Id, searchByFirstName, searchByLastName, searchByCity, searchByPhoneNumber) 
            };

            int totalNumPage = (int)Math.Ceiling((decimal)_contactRepository.GetAllContact(-1, -1, -1, applicationUser.Id, searchByFirstName, searchByLastName, searchByCity, searchByPhoneNumber)
                .ToList().Count / (decimal)recordsPerPage) - 1; //Page numering start with 0

            var res = PagePagination(homeViewModel.CurrentPage, totalNumPage); 
            homeViewModel.MinPage = res[0];
            homeViewModel.MaxPage = res[1];

            return Json(homeViewModel);
        }

        private int[] PagePagination(int currentPage, int totalNumPage)
        {
            int[] res = new int[2];

            res[0] = currentPage - 2 > 0 ? currentPage - 2 : 0;
            res[1] = currentPage + 2 < totalNumPage ? currentPage + 2 : totalNumPage;

            if (res[0] == 0 && res[1] < totalNumPage && currentPage < 2)
            {
                int k = 2 - currentPage;
                while (res[1] < totalNumPage && k > 0)
                {
                    res[1]++;
                    k--;
                }
            }
            if (res[1] == totalNumPage && res[0] > 0 && totalNumPage - currentPage < 3)
            {
                int k = 3 - (totalNumPage - currentPage);
                while (res[0] > 0 && k > 0)
                {
                    res[0]--;
                    k--;
                }
            }
            return res;
        }
    }
}