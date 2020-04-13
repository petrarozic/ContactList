using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Models.Database
{
    public class DbInitializer
    {
        public static async Task Seed(AppDbContext context, UserManager<ApplicationUser> _userManager)
        {
            if (!context.ApplicationUsers.Any())
            {
                ApplicationUser applicationUser1 = new ApplicationUser()
                {
                    UserName = "user1@user.user",
                    Email = "user1@user.user"
                };
                var result1 = await _userManager.CreateAsync(applicationUser1);
                if (result1.Succeeded)
                {
                    await _userManager.AddPasswordAsync(applicationUser1, "User123!");
                }

                ApplicationUser applicationUser2 = new ApplicationUser()
                {
                    UserName = "user2@user.user",
                    Email = "user2@user.user"
                };
                var result2 = await _userManager.CreateAsync(applicationUser2);
                if (result2.Succeeded)
                {
                    await _userManager.AddPasswordAsync(applicationUser2, "User123!");
                }
            }

            if (!context.Contacts.Any())
            {
                var contact1 = new Contact
                {
                    FirstName = "Rade",
                    LastName = "Šerbedžija",
                    City = "Bunić",
                    Note = "Filmovi: Crveno klasje, Kiklop",
                    ProfilePhoto = null
                };

                var contact2 = new Contact
                {
                    FirstName = "Goran",
                    LastName = "Višnjić",
                    City = "Šibenik",
                    Note = "Drama: Dobro došli u Sarajevo",
                    ProfilePhoto = null
                };

                var contact3 = new Contact
                {
                    FirstName = "Josipa",
                    LastName = "Lisac",
                    City = "Zagreb",
                    Note = "Prvi samostalni album Dnevnik jedne ljubavi",
                    ProfilePhoto = null
                };

                context.Contacts.AddRange(contact1, contact2, contact3);
                context.SaveChanges();

                contact1.PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber
                    {
                        Type = "Mobile",
                        Number = "098111111",
                        Note = "U nužnim situacijama"
                    },
                    new PhoneNumber
                    {
                        Type = "Mobile",
                        Number = "098111112"
                    }
                };

                contact2.PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber
                    {
                        Type = "Mobile",
                        Number = "098111113",
                        Note = "Rijetko dustupan"
                    }
                };

                contact3.PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber
                    {
                        Type = "Mobile",
                        Number = "098111114",
                        Note = "-"
                    }
                };
                context.SaveChanges();

                var contactWithLongNote = new Contact
                {
                    FirstName = "Kontakt",
                    LastName = "Bilješka",
                    City = "Grad",
                    Note = "Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd Abcd A 200",
                    ProfilePhoto = null
                };
                contactWithLongNote.PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber
                        {
                            Type = "Long note",
                            Number = "000000099",
                            Note = "Abcd Abcd Abcd Abcd Abcd Abcd Abcd Ab 40"
                        }
                    };
                context.Contacts.Add(contactWithLongNote);
                context.SaveChanges();

                var contactWithoutPhone = new Contact
                {
                    FirstName = "Ana",
                    LastName = "Anić",
                    City = "Grad",
                    Note = "Kontakt koji nema ni jedan broj",
                    ProfilePhoto = null
                };
                contactWithoutPhone.PhoneNumbers = new List<PhoneNumber>();
                context.Contacts.Add(contactWithoutPhone);
                context.SaveChanges();

                //AddUser
                var user1 = await _userManager.FindByNameAsync("user1@user.user");
                var user2 = await _userManager.FindByNameAsync("user2@user.user");

                Random random = new Random();
                switch (random.Next(1, 3))
                {
                    case 1:
                        contact1.ApplicationUser = user1;
                        break;
                    case 2:
                        contact1.ApplicationUser = user2;
                        break;
                }
                switch (random.Next(1, 3))
                {
                    case 1:
                        contact2.ApplicationUser = user1;
                        break;
                    case 2:
                        contact2.ApplicationUser = user2;
                        break;
                }
                switch (random.Next(1, 3))
                {
                    case 1:
                        contact3.ApplicationUser = user1;
                        break;
                    case 2:
                        contact3.ApplicationUser = user2;
                        break;
                }
                switch (random.Next(1, 3))
                {
                    case 1:
                        contactWithLongNote.ApplicationUser = user1;
                        break;
                    case 2:
                        contactWithLongNote.ApplicationUser = user2;
                        break;
                }
                switch (random.Next(1, 3))
                {
                    case 1:
                        contactWithoutPhone.ApplicationUser = user1;
                        break;
                    case 2:
                        contactWithoutPhone.ApplicationUser = user2;
                        break;
                }
                context.SaveChanges();

                for (var i = 0; i < 70; i++)
                {
                    var contact = new Contact
                    {
                        FirstName = "Ime" + i.ToString(),
                        LastName = "Prezime" + i.ToString(),
                        City = "Grad" + i.ToString(),
                        Note = "Bilješka" + i.ToString(),
                        ProfilePhoto = null
                    };
                    contact.PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber
                        {
                            Type = "Phone" + i.ToString(),
                            Number = "0000000" + i.ToString(),
                            Note = "Bilješka o broju .."
                        }
                    };
                    switch (random.Next(1, 3))
                    {
                        case 1:
                            contact.ApplicationUser = user1;
                            break;
                        case 2:
                            contact.ApplicationUser = user2;
                            break;
                    }
                    context.Contacts.Add(contact);
                    context.SaveChanges();
                }
            }
        }
    }
}
