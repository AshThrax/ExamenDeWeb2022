using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Film.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Acteur.Command
{
    public class UpdateActeurCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        public int FilmId { get; set; }
        public string Rolesdescription { get; set; }

        public class UpdateActeurCommandHandler : IRequestHandler<UpdateActeurCommand>
        {
            private readonly IApplicationDbContext _context;
            public UpdateActeurCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateActeurCommand command, CancellationToken cancellationToken)
            {
                var entity =await _context.Acteurs.FindAsync(command.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Acteur), command.Id);
                }
                entity.Name = command.Name;
                entity.Roles = command.Roles;
                entity.Rolesdescription = command.Rolesdescription;

                _context.Acteurs.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
