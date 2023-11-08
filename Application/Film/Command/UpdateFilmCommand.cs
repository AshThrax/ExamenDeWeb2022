using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film.Command
{
    public class UpdateFilmCommand : IRequest
    {
        public int Id { get; set; }
        public string Titre { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

        public class UpdateFilmCommandHandler : IRequestHandler<UpdateFilmCommand>
        {
            private readonly IApplicationDbContext _context;
            public UpdateFilmCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateFilmCommand Command, CancellationToken cancellationToken)
            {
                var entity = new Domain.Entities.Film
                {
                    Id = Command.Id,
                    Titre = Command.Titre,
                    Date = Command.Date,
                    Description = Command.Description,
                    Genre = Command.Genre
                };


                _context.Films.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
