using AutoMapper;
using PatikaDev.NetCore.BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.GenreOperations.Queries.GetGenreById
{
    public class GetGenreByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int genreId { get; set; }
        public GetGenreByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public GenreViewModel Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.IsActive && x.Id == genreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü sistemde kayıtlı değil!");
            GenreViewModel genreVM = _mapper.Map<GenreViewModel>(genre);
            return genreVM;
        }
    }
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
