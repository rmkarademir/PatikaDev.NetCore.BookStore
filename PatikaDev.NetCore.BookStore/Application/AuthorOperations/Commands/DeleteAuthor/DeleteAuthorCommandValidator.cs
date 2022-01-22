using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatikaDev.NetCore.BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator :AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command=> command.authorId).GreaterThan(0);
        }
    }
}
