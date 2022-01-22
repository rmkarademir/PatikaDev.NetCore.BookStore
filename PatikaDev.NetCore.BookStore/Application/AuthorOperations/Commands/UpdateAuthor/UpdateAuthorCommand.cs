using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int authorId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.Find(authorId);
            if (author is null)
                throw new InvalidOperationException("Yazar sistemde kayıtlı değil!");
            author.FirstName = string.IsNullOrEmpty(Model.FirstName) ? author.FirstName : Model.FirstName;
            author.LastName = string.IsNullOrEmpty(Model.LastName) ? author.LastName : Model.LastName;
            author.BirthDate = Model.BirthDate != default ? author.BirthDate : Model.BirthDate;
            _dbContext.SaveChanges();
        }

        public class UpdateAuthorModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }

        }
    }
}
