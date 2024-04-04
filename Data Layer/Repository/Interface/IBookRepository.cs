using DataLayer.Entity;
using SharedLayer.Core.ValueObjects;
using SharedLayer.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer.Repository.Interface
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<OperationResult<Book>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<Book>>> GetAllAsync();
        Task<OperationResult<IEnumerable<Book>>> GetByParamAsync(string name, string author, string genre);

        Task<OperationResult<IEnumerable<Book>>> GetByDescriptionAsync(string desc);
    }
}
