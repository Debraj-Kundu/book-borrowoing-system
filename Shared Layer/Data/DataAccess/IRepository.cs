using SharedLayer.Domain;
using SharedLayer.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SharedLayer.Data.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<OperationResult<DbSet<TEntity>>> GetDbSet();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
    }
}
