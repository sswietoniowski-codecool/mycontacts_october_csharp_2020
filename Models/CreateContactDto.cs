using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyContacts.WebApi.Models
{
    public class CreateContactDto
    {
        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }
        //[DataType(DataType.EmailAddress)] this is not validation, but only presentation
        [EmailAddress]
        public string Email { get; set; }
    }
}
