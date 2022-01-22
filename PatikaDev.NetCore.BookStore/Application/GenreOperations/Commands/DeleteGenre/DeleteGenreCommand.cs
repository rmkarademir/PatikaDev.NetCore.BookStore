using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int genreId { get; set; }
        public DeleteGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.Find(genreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü sistemde kayıtlı değil!");
            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }
    }
}
