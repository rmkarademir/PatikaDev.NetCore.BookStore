using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
             _dbContext = dbContext;
        }
        public void Handle(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book is null)
                throw new InvalidOperationException("Kitap sistemde kayıtlı değil!");
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges(); 
        }
    }
}
