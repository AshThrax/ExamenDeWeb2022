using Application.Post.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Post.Validation
{
    internal class CreatePostValidation : AbstractValidator<CreatePostCommand>
    {
        public CreatePostValidation()
        {
            RuleFor(x => x.Titre).MinimumLength(10).NotEmpty().NotNull();
            RuleFor(x => x.PostContent).MaximumLength(10).NotEmpty().NotNull();
        }
    }
}
