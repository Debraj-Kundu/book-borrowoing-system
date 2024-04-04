using BuisnessLayer.Domain;
using SharedLayer.Core.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuisnessLayer.BookAppServices.Interface
{
    public interface IUserService
    {
        //Task<OperationResult<IEnumerable<UserDomain>>> GetAllUsers();
        Task<OperationResult<UserDomain>> CreateUser(UserDomain item);
        Task<OperationResult<UserDomain>> GetUserWithDetails(string email, string password);
        Task<OperationResult<UserDomain>> GetUserById(int userId);

    }
}