using FluentValidation;
using PatikaDev.NetCore.BookStore.Application.BookOperations.Commands.UpdateBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.bookId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThanOrEqualTo(1).When(x=>x.Model.GenreId !=0);//UpdateBookCommand türündeki Model nesnesinin GenreId alanı 0 dan daha büyük olmalı.
            RuleFor(command => command.Model.AuthorId).GreaterThanOrEqualTo(1).When(x => x.Model.AuthorId != 0);
            //RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.Title).MinimumLength(2).When(x => x.Model.Title != string.Empty);
            //RuleFor(command => command.Model.PageCount).GreaterThan(0);
            //RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}
