using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film.Command
{
    public class DeleteFilmCommand : IRequest
    {
        public int Id { get; set; }
      

        public class DeleteFilmCommandHandler : IRequestHandler<DeleteFilmCommand>
        {
            private readonly IApplicationDbContext _context;
            public DeleteFilmCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteFilmCommand Command, CancellationToken cancellationToken)
            {
                var entity = await _context.Films.Where(x => x.Id == Command.Id).FirstOrDefaultAsync();
                if (entity == null)
                    return default;

                _context.Films.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
