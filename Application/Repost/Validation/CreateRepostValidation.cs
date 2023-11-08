using Application.Repost.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repost.Validation
{
    public class CreateRepostValidation : AbstractValidator<CreateRepostCommand>
    {
        public CreateRepostValidation()
        {
            RuleFor(x =>x.MyRepost).NotNull();
            RuleFor(x =>x.Username).NotNull();
        }
    }
}
