using PatikaDev.NetCore.BookStore.DBOperations;
using PatikaDev.NetCore.BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        public int bookId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.Find(bookId);
            if (book is null)
                throw new InvalidOperationException("Kitap sistemde kayıtlı değil!");
            book.Title = string.IsNullOrEmpty(Model.Title) ? book.Title : Model.Title;
            book.GenreId = int.Equals(Model.GenreId,0) ? book.GenreId : Model.GenreId;
            book.AuthorId = int.Equals(Model.AuthorId, 0) ? book.AuthorId : Model.AuthorId;
            //book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            //book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int AuthorId { get; set; }

            //public int PageCount { get; set; }
            //public DateTime PublishDate { get; set; }

        }
    }
}
