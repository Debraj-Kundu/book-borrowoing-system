using SharedLayer.Domain;
using Microsoft.EntityFrameworkCore;
using SharedLayer.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Data.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : DomainBase, IDomain
    {

        protected DbContext dbContext;
        protected readonly DbSet<TEntity> dbset;

        protected Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
        }

        public virtual void DeleteAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<OperationResult<DbSet<TEntity>>> GetDbSet()
        {
            var result = dbContext.Set<TEntity>();
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<DbSet<TEntity>>(result, true, message);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }
    }
}
