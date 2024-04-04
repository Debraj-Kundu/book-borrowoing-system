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
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookDomainDbContext Context { get; }

        public BookRepository(BookDomainDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<OperationResult<IEnumerable<Book>>> GetAllAsync()
        {
            IEnumerable<Book> books = Context.Books
                                        .Include(b => b.LentByUser)
                                        .Include(b => b.CurrentBorrowedByUser)
                                        .ToList();
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<IEnumerable<Book>>(books, true, message);
        }
        public override async Task AddAsync(Book entity)
        {
            await base.AddAsync(entity);
        }
        public override void DeleteAsync(Book entity)
        {
            base.DeleteAsync(entity);
        }
        public override async Task UpdateAsync(Book entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await base.UpdateAsync(entity);
        }


        public async Task<OperationResult<IEnumerable<Book>>> GetByParamAsync(string name, string author, string genre)
        {
            if (name == null) name = "";
            if (author == null) author = "";
            if (genre == null) genre = "";

            var entity = Context.Books
                            .Where(e => ((name.Count() > 0 ? e.Name.Contains(name) : true) && 
                                    (author.Count() >0 ? e.Author.Contains(author) : true) && 
                                        (genre.Count() >0 ? e.Genre.Contains(genre) : true)));

            var result = await entity
                                .Include(b => b.LentByUser)
                                .Include(b => b.CurrentBorrowedByUser)
                                .ToListAsync();
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<IEnumerable<Book>>(result, true, message);
        }


        public async Task<OperationResult<IEnumerable<Book>>> GetByDescriptionAsync(string desc)
        {
            var result = await Context.Books.Where(e => e.Description.Equals(desc)).ToListAsync();
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<IEnumerable<Book>>(result, true, message);
        }

        public async Task<OperationResult<Book>> GetByIdAsync(int id)
        {
            var entity = Context.Books
                                .Where(b => b.Id.Equals(id));
            var result = await entity
                                .Include(b => b.LentByUser)
                                .Include(b => b.CurrentBorrowedByUser)
                                .FirstOrDefaultAsync();
                                
            Message message = new Message(string.Empty, "Return Successfully");
            return new OperationResult<Book>(result, true, message);
        }
    }
}
