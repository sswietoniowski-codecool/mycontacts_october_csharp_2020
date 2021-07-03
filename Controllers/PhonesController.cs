using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyContacts.WebApi.Infrastructure;

namespace MyContacts.WebApi.Controllers
{
    [ApiController]
    [Route("api/contacts/{contactId:int}/phones")]
    public class PhonesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPhones(int contactId)
        {
            var contactDto = DataService.Current.Contacts.FirstOrDefault(c => c.Id == contactId);

            if (contactDto == null)
            {
                return NotFound();
            }

            return Ok(contactDto.Phones);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPhone(int contactId, int id)
        {
            var contactDto = DataService.Current.Contacts.FirstOrDefault(c => c.Id == contactId);

            if (contactDto == null)
            {
                return NotFound();
            }

            var phoneDto = contactDto.Phones.FirstOrDefault(p => p.Id == id);

            if (phoneDto == null)
            {
                return NotFound();
            }

            return Ok(phoneDto);
        }
    }
}
