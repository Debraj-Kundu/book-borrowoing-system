using SharedLayer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entity
{
    public class User : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int Tokens_Available { get; set; }

        public virtual ICollection<Book>? Books_Borrowed { get; set; }
        
        public virtual ICollection<Book>? Books_Lent { get; set; }

    }
}
