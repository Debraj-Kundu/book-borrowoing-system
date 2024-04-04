using DataLayer.DataContext;
using DataLayer.Repository.Implementation;
using DataLayer.Repository.Interface;
using SharedLayer.Core.ExceptionManagement;
using SharedLayer.Data.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.UoW
{
    public class BookUnitOfWork : UnitOfWork, IBookUnitOfWork
    {

        public BookUnitOfWork(BookDomainDbContext context, IBookRepository bookRepository, IUserRepository userRepository, IExceptionManager exceptionManager) : base(context, exceptionManager)
        {
            BookRepository = bookRepository;
            UserRepository = userRepository;
        }

        public IBookRepository BookRepository { get; }

        public IUserRepository UserRepository { get; }
    }
}
