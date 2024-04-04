using SharedLayer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Entity
{
    public class Book : DomainBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Genre { get; set; }

        public bool Is_Book_Available { get; set; }

        [Required]
        public string Description { get; set; }

        public int Lent_By_User_id { get; set; }
        
        public virtual User LentByUser { get; set; }


        public int? Currently_Borrowed_By_User_Id { get; set; }
        
        public virtual User? CurrentBorrowedByUser { get; set; }

        //[Required]
        //public int AvailableQuantity { get; set; }

        public string? BookImage { get; set; }

    }
}
