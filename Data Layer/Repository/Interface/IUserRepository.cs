using DataLayer.Entity;
using SharedLayer.Core.ValueObjects;
using SharedLayer.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<OperationResult<User>> CheckForUser(string email, string password);
        Task<OperationResult<User>> GetByIdAsync(int userId);
    }
}
