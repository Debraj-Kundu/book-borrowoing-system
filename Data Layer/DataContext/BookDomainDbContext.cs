using DataLayer.Entity;
using SharedLayer.Core.ExceptionManagement.CustomException;
using SharedLayer.Core.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DataLayer.DataContext
{
    public class BookDomainDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public BookDomainDbContext(DbContextOptions<BookDomainDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(c => c.Username)
                .IsUnique(true);
            builder.Entity<Book>()
                .HasIndex(p => p.Name)
                .IsUnique(true);
            builder.Entity<Book>()
                .HasOne(b => b.LentByUser)
                .WithMany(u => u.Books_Lent)
                .HasForeignKey(b => b.Lent_By_User_id)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Book>()
                .HasOne(b => b.CurrentBorrowedByUser)
                .WithMany(u => u.Books_Borrowed)
                .HasForeignKey(b => b.Currently_Borrowed_By_User_Id)
                .OnDelete(DeleteBehavior.Restrict);

            new DbInitializer(builder).Seed();
        }

        public override int SaveChanges()
        {
            string errorMessage = string.Empty;
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            List<ValidationException> validationExceptionList = new List<ValidationException>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults, true))
                {
                    foreach (var result in validationResults)
                    {
                        if (result != ValidationResult.Success)
                        {
                            validationExceptionList.Add(new ValidationException(result.ErrorMessage));
                        }
                    }

                    throw new ValidationExceptions(validationExceptionList);
                }
            }

            return base.SaveChanges();
        }
    }
    class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<User>().HasData(
                   new User() { Id = 1, Name = "Userone", Username = "User1", Password = CommonMethods.Encrypt("User@123"), Tokens_Available = 1, Books_Borrowed = null, Books_Lent = null, CreatedOnDate = DateTimeOffset.UtcNow, ModifiedOnDate = DateTimeOffset.UtcNow, IsDeleted = false },
                   
                   new User() { Id = 2, Name = "Usertwo", Username = "User2", Password = CommonMethods.Encrypt("User@123"), Tokens_Available = 1, Books_Borrowed = null, Books_Lent = null, CreatedOnDate = DateTimeOffset.UtcNow, ModifiedOnDate = DateTimeOffset.UtcNow, IsDeleted = false },

                   new User() { Id = 3, Name = "Userthree", Username = "User3", Password = CommonMethods.Encrypt("User@123"), Tokens_Available = 1, Books_Borrowed = null, Books_Lent = null, CreatedOnDate = DateTimeOffset.UtcNow, ModifiedOnDate = DateTimeOffset.UtcNow, IsDeleted = false },

                   new User() { Id = 4, Name = "Userfour", Username = "User4", Password = CommonMethods.Encrypt("User@123"), Tokens_Available = 1, Books_Borrowed = null, Books_Lent = null, CreatedOnDate = DateTimeOffset.UtcNow, ModifiedOnDate = DateTimeOffset.UtcNow, IsDeleted = false }
            );
        }
    }
}
