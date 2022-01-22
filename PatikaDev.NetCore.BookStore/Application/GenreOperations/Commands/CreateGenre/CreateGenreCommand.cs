using AutoMapper;
using PatikaDev.NetCore.BookStore.DBOperations;
using PatikaDev.NetCore.BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre is not null)
                throw new InvalidOperationException("Kitap türü sistemde kayıtlı!");
            genre = _mapper.Map<Genre>(Model); //Model ile gelen bilgileri Genre nesnesine map et/esle. 
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }
        public class CreateGenreModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
