using Application.Acteur.Command;
using Application.Repost.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repost.Validation
{
    public class UpdateRepostValidation : AbstractValidator<UpdateRepostCommand>
    {
        public UpdateRepostValidation()
        {
            RuleFor(x => x.MyRepost).NotNull();
            RuleFor(x => x.PostId).NotNull();

        }
    }
}
