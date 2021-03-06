using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int bookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
             _dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.Find(bookId);
            if (book is null)
                throw new InvalidOperationException("Kitap sistemde kayıtlı değil!");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges(); 
        }
    }
}
