﻿using PatikaDev.NetCore.BookStore.DBOperations;
using PatikaDev.NetCore.BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book is null)
                throw new InvalidOperationException("Kitap sistemde kayıtlı değil!");
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            _dbContext.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
            public int GenreId { get; set; }
        }
    }
}