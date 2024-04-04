using AutoMapper;
using BuisnessLayer.Domain;
using BuisnessLayer.BookAppServices.Interface;
using DataLayer.Entity;
using DataLayer.UoW;
using SharedLayer.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BuisnessLayer.BookAppServices.Implementation
{
    public class BookService : IBookService
    {
        public IBookUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }

        public BookService(IBookUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<BookDomain>>> GetAllBooks()
        {
            IEnumerable<BookDomain> result = new List<BookDomain>();
            var books = await UnitOfWork.BookRepository.GetAllAsync();
            if (books.Data?.Any() == true)
            {
                result = Mapper.Map<IEnumerable<BookDomain>>(books.Data).ToList();
            }
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<IEnumerable<BookDomain>>(result, true, message);
        }

        public async Task<OperationResult<BookDomain>> CreateBook(BookDomain item)
        {
            Book book = Mapper.Map<BookDomain, Book>(item);
            book.CreatedOnDate = DateTimeOffset.Now;
            book.Name = item.Name;
            book.Lent_By_User_id = item.Lent_By_User_id;

            await UnitOfWork.BookRepository.AddAsync(book);

            item.Id = book.Id;

            OperationResult result;

            result = await UnitOfWork.Commit();

            return new OperationResult<BookDomain>(item, result.IsSuccess, result.MainMessage, result.AssociatedMessages.ToList<Message>());
        }

        public async Task<OperationResult<BookDomain>> GetBookById(int id)
        {
            var book = await UnitOfWork.BookRepository.GetByIdAsync(id);
            BookDomain result = Mapper.Map<BookDomain>(book.Data);

            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<BookDomain>(result, true, message);
        }


        public async Task<OperationResult<IEnumerable<BookDomain>>> GetBookByParam(string name, string author, string genre)
        {
            var book = await UnitOfWork.BookRepository.GetByParamAsync(name, author, genre);
            var result = Mapper.Map<IEnumerable<BookDomain>>(book.Data);

            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<IEnumerable<BookDomain>>(result, true, message);
        }

        public Task<OperationResult<IEnumerable<BookDomain>>> GetBookByDescription(string desc)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> BorrowBook(int bookId, int borrowerId)
        {
            OperationResult result;
            Message message;
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var bookEntity = (await UnitOfWork.BookRepository.GetByIdAsync(bookId)).Data;
                var borrowerUserEntity = (await UnitOfWork.UserRepository.GetByIdAsync(borrowerId)).Data;

                int lenderId = bookEntity.Lent_By_User_id;
                var lenderUserEntity = (await UnitOfWork.UserRepository.GetByIdAsync(lenderId)).Data;


                if (bookEntity != null && lenderUserEntity != null && borrowerUserEntity != null)
                {
                    if(!bookEntity.Is_Book_Available)
                        return new OperationResult(false, new Message(string.Empty, "Book is currently not available to borrow"));
                    if(borrowerUserEntity.Tokens_Available == 0)
                        return new OperationResult(false, new Message(string.Empty, "Not enough tokens"));

                    lenderUserEntity.Tokens_Available += 1;
                    lenderUserEntity.ModifiedOnDate = DateTimeOffset.Now;

                    borrowerUserEntity.Tokens_Available -= 1;
                    borrowerUserEntity.ModifiedOnDate = DateTimeOffset.Now;

                    bookEntity.Currently_Borrowed_By_User_Id = borrowerId;
                    bookEntity.Is_Book_Available = false;
                    bookEntity.ModifiedOnDate = DateTimeOffset.Now;
                    
                    await UnitOfWork.BookRepository.UpdateAsync(bookEntity);
                    await UnitOfWork.UserRepository.UpdateAsync(lenderUserEntity);
                    await UnitOfWork.UserRepository.UpdateAsync(borrowerUserEntity);

                    await transaction.CommitAsync();
                    
                }
                else if(bookEntity != null && !bookEntity.Is_Book_Available)
                    return new OperationResult(false, new Message(string.Empty, "Book is currently not available to borrow"));
                else
                    await transaction.RollbackAsync();

                result = await UnitOfWork.Commit();
            }

            message = new Message(string.Empty, "Borrowed successfully");
            return new OperationResult(result.IsSuccess, message);
        }

        public async Task<OperationResult> ReturnBook(int bookId, int borrowerId)
        {
            var bookEntity = (await UnitOfWork.BookRepository.GetByIdAsync(bookId)).Data;

            if (bookEntity == null)
                return new OperationResult(false, new Message(string.Empty, "Unexpected error occured. Book was not returned"));
            if (bookEntity.Is_Book_Available)
                return new OperationResult(false, new Message(string.Empty, "Can not return book, you need to borrow first"));
            if (bookEntity.Currently_Borrowed_By_User_Id != borrowerId)
                return new OperationResult(false, new Message(string.Empty, "You are not authorized to return the book"));

            bookEntity.Currently_Borrowed_By_User_Id = null;
            bookEntity.Is_Book_Available = true;
            bookEntity.ModifiedOnDate = DateTimeOffset.Now;

            await UnitOfWork.BookRepository.UpdateAsync(bookEntity);


            OperationResult result = await UnitOfWork.Commit();


            Message message = new Message(string.Empty, "Book returned successfully");
            return new OperationResult(result.IsSuccess, message);
        }
        public async Task<OperationResult> UpdateBook(int bookId, BookDomain book, int userId)
        {
            var entityToUpdate = (await UnitOfWork.BookRepository.GetByIdAsync(bookId)).Data;
            var lenderId = entityToUpdate.Lent_By_User_id;
            if (lenderId != userId)
                return new OperationResult(false, new Message("401", "You are not authorized to edit the book"));

            //entityToUpdate.Rating = book.Rating;
            Mapper.Map(book, entityToUpdate);
            entityToUpdate.ModifiedOnDate = DateTimeOffset.Now;

            await UnitOfWork.BookRepository.UpdateAsync(entityToUpdate);

            OperationResult result = await UnitOfWork.Commit();


            Message message = new Message(string.Empty, "Book updated successfully");
            return new OperationResult(result.IsSuccess, message);

        }

        public async Task<OperationResult<BookDomain>> RemoveBook(int bookId)
        {
            var book = await UnitOfWork.BookRepository.GetByIdAsync(bookId);
            if (book.Data == null)
            {
                Message errMsg = new Message(string.Empty, "Not Found");
                return new OperationResult<BookDomain>(null, false, errMsg);
            }
            UnitOfWork.BookRepository.DeleteAsync(book.Data);
            Message message = new Message(string.Empty, "Deleted Successfully");
            await UnitOfWork.Commit();
            return new OperationResult<BookDomain>(null, true, message);
        }

    }
}
