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
    public class GetAllFilmQuery : IRequest<FilmVm>
    {
        public class GetAllFilmQueryHandler : IRequestHandler<GetAllFilmQuery, FilmVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public GetAllFilmQueryHandler(IApplicationDbContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FilmVm> Handle(GetAllFilmQuery query, CancellationToken cancellationToken)
            {
                return new FilmVm
                {
                    Lists = await _context.Films
                    .ProjectTo<FilmDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Titre)
                    .ToListAsync(cancellationToken)
                };


            }
        }
    }
}
