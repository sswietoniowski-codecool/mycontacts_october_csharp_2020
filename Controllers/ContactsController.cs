using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyContacts.WebApi.Infrastructure;
using MyContacts.WebApi.Models;

namespace MyContacts.WebApi.Controllers
{
    [ApiController]
    //[Route("api/contacts")]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        // GET http://localhost:33333/api/contacts
        // GET http://localhost:33333/api/contacts?like=ski
        [HttpGet]
        public IActionResult GetContacts([FromQuery] string like)
        {
            var contactsQuery = DataService.Current.Contacts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(like))
            {
                contactsQuery = contactsQuery.Where(c => c.Name.Contains(like));
            }

            return Ok(contactsQuery.ToList());
        }

        // GET http://localhost:33333/api/contacts/1
        [HttpGet("{id:int}", Name = "GetContact")]
        public IActionResult GetContact(int id)
        {
            var contactDto = DataService.Current.Contacts.FirstOrDefault(c => c.Id == id);

            if (contactDto == null)
            {
                return NotFound();
            }

            return Ok(contactDto);
        }

        // POST http://localhost:33333/api/contacts
        [HttpPost]
        public IActionResult CreateContact([FromBody] CreateContactDto createContactDto)
        {
            var maxId = DataService.Current.Contacts.Max(c => c.Id);

            var contactDto = new ContactDto
            {
                Id = maxId + 1,
                FirstName = createContactDto.FirstName,
                LastName = createContactDto.LastName,
                Email = createContactDto.Email
            };

            DataService.Current.Contacts.Add(contactDto);

            return CreatedAtRoute("GetContact", new {id = contactDto.Id}, contactDto);
        }
    }
}
