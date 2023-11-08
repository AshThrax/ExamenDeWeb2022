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
    public class GetFilmByIdQuery : IRequest<FilmDto>
    {
        public int Id { get; set; }
        public class GetFilmByIdQueryHandler : IRequestHandler<GetFilmByIdQuery, FilmDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public GetFilmByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FilmDto> Handle(GetFilmByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = new FilmDto();
                { 
                
                }
                entity = await _context.Films.ProjectTo<FilmDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(x =>x.Id==query.Id);
                return entity;

            }
        }
    }
}
