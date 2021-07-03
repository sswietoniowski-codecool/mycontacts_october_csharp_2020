using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyContacts.WebApi.Models;

namespace MyContacts.WebApi.Infrastructure
{
    public class DataService
    {
        public static DataService Current { get; } = new DataService();
        public List<ContactDto> Contacts { get; set; }

        public DataService()
        {
            Contacts = new List<ContactDto>()
            {
                new ContactDto
                {
                    Id = 1,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "jkowalski@unknown.com",
                    Phones = new List<PhoneDto>()
                    {
                        new PhoneDto
                        {
                            Id = 1,
                            Number = "111-111-1111",
                            Description = "Domowy"
                        },
                        new PhoneDto
                        {
                            Id = 1,
                            Number = "222-222-2222",
                            Description = "Praca"
                        }

                    }
                },
                new ContactDto
                {
                    Id = 2,
                    FirstName = "Anna",
                    LastName = "Nowak",
                    Email = "anowak@unknown.com"
                },
                new ContactDto
                {
                    Id = 3,
                    FirstName = "Adam",
                    LastName = "Kowalski",
                    Email = "akowalski@unknown.com"
                }
            };
        }
    }
}
