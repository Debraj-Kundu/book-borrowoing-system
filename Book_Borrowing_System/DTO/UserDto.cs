using DataLayer.Entity;
using SharedLayer.Domain;
using SharedLayer.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Book_Borrowing_System.DTO
{
    public class UserDto : DtoBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int Tokens_Available { get; set; }

        public virtual ICollection<BookDto>? Books_Borrowed { get; set; }

        public virtual ICollection<BookDto>? Books_Lent { get; set; }
    }
}
