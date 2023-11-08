using Application.Acteur.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Acteur.Validation
{
    public class CreateActeurValidation : AbstractValidator<CreateActeurCommand>
    {
        public CreateActeurValidation()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull();
            RuleFor(c=>c.Roles).NotEmpty();
            RuleFor(c=>c.Rolesdescription).NotEmpty();
        }
    }
}
