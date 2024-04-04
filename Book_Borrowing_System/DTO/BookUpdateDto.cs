using SharedLayer.Service;
using System.ComponentModel.DataAnnotations;

namespace Book_Borrowing_System.DTO
{
    public class BookUpdateDto: DtoBase
    {
        public string? Name { get; set; }

        public int? Rating { get; set; }

        public string? Author { get; set; }

        public string? Genre { get; set; }

        public bool Is_Book_Available { get; set; } = true;

        public string? Description { get; set; }

        public int? Lent_By_User_id { get; set; }

        public virtual UserDto? LentByUser { get; set; }


        public int? Currently_Borrowed_By_User_Id { get; set; }

        public virtual UserDto? CurrentBorrowedByUser { get; set; }

 
        //public int AvailableQuantity { get; set; }

        public string? BookImage { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
