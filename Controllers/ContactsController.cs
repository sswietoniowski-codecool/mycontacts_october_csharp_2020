using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyContacts.WebApi.Infrastructure;

namespace MyContacts.WebApi.Controllers
{
    [ApiController]
    //[Route("api/contacts")]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        // GET http://localhost:33333/api/contacts
        [HttpGet]
        public IActionResult GetContacts()
        {
            var contactsDto = DataService.Current.Contacts;

            return Ok(contactsDto);
        }

        // GET http://localhost:33333/api/contacts/1
        [HttpGet("{id:int}")]
        public IActionResult GetContact(int id)
        {
            var contactDto = DataService.Current.Contacts.FirstOrDefault(c => c.Id == id);

            if (contactDto == null)
            {
                return NotFound();
            }

            return Ok(contactDto);
        }
    }
}
