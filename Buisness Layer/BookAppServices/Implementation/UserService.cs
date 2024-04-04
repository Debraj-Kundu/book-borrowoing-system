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

namespace BuisnessLayer.BookAppServices.Implementation
{
    public class UserService : IUserService
    {
        public IBookUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }
        public UserService(IBookUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<OperationResult<UserDomain>> CreateUser(UserDomain item)
        {
            User user = Mapper.Map<UserDomain, User>(item);
            user.CreatedOnDate = DateTimeOffset.Now;
            if(user.Tokens_Available==0)    user.Tokens_Available = 10;
            await UnitOfWork.UserRepository.AddAsync(user);

            item.Id = user.Id;

            OperationResult result;

            result = await UnitOfWork.Commit();

            return new OperationResult<UserDomain>(item, result.IsSuccess, result.MainMessage, result.AssociatedMessages.ToList<Message>());
        }

        public async Task<OperationResult<UserDomain>> GetUserWithDetails(string username, string password)
        {
            var book = await UnitOfWork.UserRepository.CheckForUser(username, password);
            var result = Mapper.Map<UserDomain>(book.Data);

            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<UserDomain>(result, true, message);
        }

        public async Task<OperationResult<UserDomain>> GetUserById(int userId)
        {
            var book = await UnitOfWork.UserRepository.GetByIdAsync(userId);
            UserDomain result = Mapper.Map<UserDomain>(book.Data);

            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<UserDomain>(result, true, message);
        }
    }
}
