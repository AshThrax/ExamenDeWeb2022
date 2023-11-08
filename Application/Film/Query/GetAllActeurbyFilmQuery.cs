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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Film.Query
{
    public  class GetAllActeurbyFilmQuery:IRequest<List<ActeurDto>>
    {
        public int filmId { get; set; }
       
    } 
    public class GetAllActeurbyFilmQueryHandler:IRequestHandler<GetAllActeurbyFilmQuery,List<ActeurDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public GetAllActeurbyFilmQueryHandler(IApplicationDbContext _context, IMapper _mapper)
            {
                this._context= _context;
                this._mapper= _mapper;
            }

            public async Task<List<ActeurDto>> Handle(GetAllActeurbyFilmQuery request, CancellationToken cancellationToken)
            {
                
                var entity = await _context.Acteurs
                    .ProjectTo<ActeurDto>(_mapper.ConfigurationProvider)
                    .Where(c => c.FilmId == request.filmId)
                    .ToListAsync(cancellationToken);


                return entity;
            }
    }
}
