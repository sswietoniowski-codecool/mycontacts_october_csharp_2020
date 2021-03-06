using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
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
            if (createContactDto.FirstName == createContactDto.LastName)
            {
                ModelState.AddModelError(
                        key: "Description",
                        errorMessage: "It is unlikely that first name and last name are the same ;-)"
                    );
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] UpdateContactDto updateContactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactDto = DataService.Current.Contacts.FirstOrDefault(c => c.Id == id);

            if (contactDto == null)
            {
                return NotFound();
            }

            contactDto.FirstName = updateContactDto.FirstName;
            contactDto.LastName = updateContactDto.LastName;
            contactDto.Email = updateContactDto.Email;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contactDto = DataService.Current.Contacts.FirstOrDefault(c => c.Id == id);

            if (contactDto == null)
            {
                return NotFound();
            }

            DataService.Current.Contacts.Remove(contactDto);

            return NoContent();
        }

        // PATCH api/contacts/1
        // http://jsonpatch.com/
        // [
        // {
        //     "op": "add",
        //     "path": "/name",
        //     "value": "new name"
        // },
        // {
        //     "op": "replace",
        //     "path": "/description",
        //     "value": "new description"
        // }
        // ]   
        [HttpPatch("{id}")]
        public IActionResult PartialUpdateContact(int id, [FromBody] JsonPatchDocument<UpdateContactDto> patchDocument)
        {
            var contactDto = DataService.Current.Contacts.FirstOrDefault(c => c.Id == id);

            if (contactDto == null)
            {
                return NotFound();
            }

            var contactToBePatched = new UpdateContactDto()
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                Email = contactDto.Email
            };

            patchDocument.ApplyTo(contactToBePatched);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (contactToBePatched.FirstName == contactToBePatched.LastName)
            {
                ModelState.AddModelError(
                    key: "Description",
                    errorMessage: "It is unlikely that first name and last name are the same ;-)"
                );
            }

            if (!TryValidateModel(contactToBePatched))
            {
                return BadRequest();
            }

            contactDto.FirstName = contactToBePatched.FirstName;
            contactDto.LastName = contactToBePatched.LastName;
            contactDto.Email = contactToBePatched.Email;

            return NoContent();
        }
    }
}
