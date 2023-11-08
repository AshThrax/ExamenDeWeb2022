using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Acteur.Command
{
    public class DeleteActeurCommand : IRequest
    {
        public int Id { get; set; }
       
        
        public class DeleteActeurCommandHandler : IRequestHandler<DeleteActeurCommand>
        {
            private readonly IApplicationDbContext _context;


            public DeleteActeurCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteActeurCommand Command, CancellationToken cancellationToken)
            {
                var entity = await _context.Acteurs
                    .Where(x => x.Id == Command.Id)
                    .SingleOrDefaultAsync(cancellationToken);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Acteur), Command.Id);
                }


                _context.Acteurs.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
