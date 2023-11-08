using Application.Film.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film.Validation
{
    public class UpdateFilmValidation : AbstractValidator<UpdateFilmCommand>
    {
        public UpdateFilmValidation()
        {
            RuleFor(v => v.Id).NotEmpty().NotNull();
            RuleFor(V => V.Titre).NotEmpty();
            RuleFor(V => V.Genre).MaximumLength(200).NotEmpty();
            RuleFor(v => v.Date).NotEmpty();
            RuleFor(v =>v.Description).MinimumLength(20).NotEmpty();
        }
    }
}
