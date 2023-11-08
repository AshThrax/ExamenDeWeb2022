using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Acteur.Command
{
    public class CreateActeurCommand : IRequest<int>
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        public string Rolesdescription { get; set; }



        public class CreateActeurCommandHandler : IRequestHandler<CreateActeurCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateActeurCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateActeurCommand command, CancellationToken cancellationToken)
            {
                var entity = new Domain.Entities.Acteur
                {
                    FilmId= command.FilmId,
                    Name = command.Name,
                    Roles = command.Roles ,
                    Rolesdescription = command.Rolesdescription
                    
                };

                _context.Acteurs.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}
