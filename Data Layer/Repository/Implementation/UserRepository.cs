using DataLayer.DataContext;
using DataLayer.Entity;
using DataLayer.Repository.Interface;
using SharedLayer.Core.ValueObjects;
using SharedLayer.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public BookDomainDbContext Context { get; }

        public UserRepository(BookDomainDbContext context) : base(context)
        {
            Context = context;
        }

        public override async Task AddAsync(User entity)
        {
            await base.AddAsync(entity);
        }

        public override void DeleteAsync(User entity)
        {
            base.DeleteAsync(entity);
        }


        public override async Task UpdateAsync(User entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<OperationResult<User>> CheckForUser(string username, string password)
        {
            var result = await Context.Users.Where(
                e => e.Username.Equals(username) && e.Password.Equals(password)
                ).FirstOrDefaultAsync();
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<User>(result, true, message);
        }

        public async Task<OperationResult<User>> GetByIdAsync(int userId)
        {
            var entity = Context.Users
                                .Where(b => b.Id.Equals(userId));
            var result = await entity
                                .Include(u => u.Books_Lent)
                                .Include(u => u.Books_Borrowed)
                                .FirstOrDefaultAsync();

            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<User>(result, true, message);
        }
    }
}
