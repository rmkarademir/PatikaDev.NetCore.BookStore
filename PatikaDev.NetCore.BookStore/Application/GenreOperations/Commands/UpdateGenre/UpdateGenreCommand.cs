using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model { get; set; }
        public int genreId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.Find(genreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü sistemde kayıtlı değil!");

            if (_dbContext.Genres.Any(x=> x.Name.ToLower() == Model.Name.ToLower() && x.Id != genreId))
                throw new InvalidOperationException("Aynı kitap türü sistemde kayıtlı!");

            genre.Name = string.IsNullOrEmpty(Model.Name) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _dbContext.SaveChanges();
        }

        public class UpdateGenreModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; } = true;
        }
    }
}
