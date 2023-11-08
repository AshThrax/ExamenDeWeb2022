using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film.Command
{
    public class CreateFilmCommand : IRequest<int>
    {
        public string Titre { get; set; }

        public DateTime Date { get; set; }
     
        public string Description { get; set; }
        public string Genre { get; set; }
       

        public class CreateFilmCommandHandler : IRequestHandler<CreateFilmCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateFilmCommandHandler(IApplicationDbContext context)
            {
                _context = context
 ;
            }
            public async Task<int> Handle(CreateFilmCommand command, CancellationToken cancellationToken)
            {
                var entity = new Domain.Entities.Film
                {
                    Titre = command.Titre,
                    Date = command.Date,
                    Description = command.Description,
                    Genre = command.Genre,
                    
                   
                };

                _context.Films.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;

            }
        }
    }
}
