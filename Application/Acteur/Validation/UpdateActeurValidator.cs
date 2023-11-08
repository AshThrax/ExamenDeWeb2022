using Application.Acteur.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Acteur.Validation
{
    public class UpdateActeurValidator:AbstractValidator<UpdateActeurCommand>
    {
        public UpdateActeurValidator()
        {
            RuleFor(v => v.FilmId).NotNull().NotEmpty();
            RuleFor(V => V.Name).NotEmpty();
            RuleFor(V => V.Rolesdescription).MaximumLength(200).NotEmpty();
            RuleFor(v => v.Roles).NotEmpty();
        }
    }
}
