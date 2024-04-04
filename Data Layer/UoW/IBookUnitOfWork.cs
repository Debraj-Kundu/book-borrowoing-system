using DataLayer.Repository.Interface;
using SharedLayer.Data.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.UoW
{
    public interface IBookUnitOfWork : IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IUserRepository UserRepository { get; }

    }
}
