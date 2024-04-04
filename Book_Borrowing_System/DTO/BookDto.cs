using SharedLayer.Domain;
using SharedLayer.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DataLayer.Entity;

namespace Book_Borrowing_System.DTO
{
    public class BookDto : DtoBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Genre { get; set; }

        public bool Is_Book_Available { get; set; } = true;

        [Required]
        public string Description { get; set; }

        public int? Lent_By_User_id { get; set; }

        public virtual UserDto? LentByUser { get; set; }


        public int? Currently_Borrowed_By_User_Id { get; set; }

        public virtual UserDto? CurrentBorrowedByUser { get; set; }

        //[Required]
        //public int AvailableQuantity { get; set; }

        public string? BookImage { get; set; }

        public IFormFile? ImageFile { get; set; }

    }
}
