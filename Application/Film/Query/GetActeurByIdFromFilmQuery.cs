using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film.Query
{
    public class GetActeurByIdFromFilmQuery : IRequest<ActeurDto>
    {
        public int filmId { get; set; }
        public int ActeurId {get; set;}

        public class GetActeurByIdFromFilmQueryHandler : IRequestHandler<GetActeurByIdFromFilmQuery, ActeurDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public GetActeurByIdFromFilmQueryHandler(IApplicationDbContext _context, IMapper _mapper)
            {
                this._context = _context;
                this._mapper = _mapper;
            }
            public async Task<ActeurDto> Handle(GetActeurByIdFromFilmQuery request, CancellationToken cancellationToken)
            {

                var entity = await _context.Films
                    .ProjectTo<FilmDto>(_mapper.ConfigurationProvider)
                    .Where(x=>x.Id ==request.filmId)
                    .FirstOrDefaultAsync(cancellationToken);

                var ActorEntity = entity.Acteurs
                                        .FirstOrDefault(x => x.Id == request.ActeurId);

                return ActorEntity;
            }
        }

    }
}
