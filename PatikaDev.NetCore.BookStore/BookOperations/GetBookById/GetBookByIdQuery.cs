using PatikaDev.NetCore.BookStore.Common;
using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBookByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BooksViewModel Handle(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book is null)
                throw new InvalidOperationException("Kitap sistemde kayıtlı değil!");
            BooksViewModel _book = new BooksViewModel();
            _book.Title = book.Title;
            _book.Genre = ((GenreEnum)book.GenreId).ToString();
            _book.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            _book.PageCount = book.PageCount;
 
            return _book;
        }
    }
    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }

}

