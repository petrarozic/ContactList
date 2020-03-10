using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Models.Database
{
    public class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Contacts.Any())
            {
                var contact1 = new Contact
                {
                    FirstName = "Rade",
                    LastName = "Šerbedžija",
                    City = "Bunić",
                    Note = "Filmovi: Crveno klasje, Kiklop"
                };

                var contact2 = new Contact
                {
                    FirstName = "Goran",
                    LastName = "Višnjić",
                    City = "Šibenik",
                    Note = "Drama: Dobro došli u Sarajevo"
                };

                var contact3 = new Contact
                {
                    FirstName = "Josipa",
                    LastName = "Lisac",
                    City = "Zagreb",
                    Note = "Prvi samostalni album Dnevnik jedne ljubavi"
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
                        Number = "098111113",
                        Note = "Rijetko dustupan"
                    }
                };
                context.SaveChanges();
            }
        }
    }
}
