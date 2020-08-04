using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreLibrary.Entities.Resources
{
    public class SaveUserResource
    {
        [Required]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
