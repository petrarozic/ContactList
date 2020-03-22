using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Interfaces;
using Contacts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public HomeController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Contacts = _contactRepository.GetAllContact(0, 5),

                ContactsPerPage = 5, 
                CurrentPage = 0, 
                MinPage = 0
            };

            int totalNumPage = (int)Math.Ceiling((decimal)_contactRepository.GetAllContact().ToList().Count / (decimal)5) -1;//Page numering start with 0
            homeViewModel.MaxPage = totalNumPage < 4 ? totalNumPage : 4;

            return View(homeViewModel);
        }

        [HttpGet]
        public JsonResult Filter (int goToPage, int recordsPerPage)
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                CurrentPage = goToPage,
                ContactsPerPage = recordsPerPage,

                Contacts = _contactRepository.GetAllContact(goToPage, recordsPerPage) 
            };

            int totalNumPage = (int)Math.Ceiling((decimal)_contactRepository.GetAllContact()
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