using BuisnessLayer.Domain;
using SharedLayer.Core.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuisnessLayer.BookAppServices.Interface
{
    public interface IBookService
    {
        Task<OperationResult> UpdateBook(int bookId, BookDomain book, int userId);
        Task<OperationResult<BookDomain>> GetBookById(int id);
        Task<OperationResult<BookDomain>> RemoveBook(int bookId);
        Task<OperationResult<IEnumerable<BookDomain>>> GetAllBooks();
        Task<OperationResult<BookDomain>> CreateBook(BookDomain item);
        Task<OperationResult> BorrowBook(int bookId, int borrowerId);
        Task<OperationResult> ReturnBook(int bookId, int borrowerId);

        Task<OperationResult<IEnumerable<BookDomain>>> GetBookByParam(string name,string author, string genre);

        Task<OperationResult<IEnumerable<BookDomain>>> GetBookByDescription(string desc);
    }
}