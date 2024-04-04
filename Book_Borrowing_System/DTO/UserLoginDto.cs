using DataLayer.Entity;
using SharedLayer.Domain;
using SharedLayer.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Book_Borrowing_System.DTO
{
    public class UserLoginDto : DtoBase
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
