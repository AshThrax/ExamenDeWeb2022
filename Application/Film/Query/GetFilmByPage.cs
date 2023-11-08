using Application.Common.Interfaces;
using Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film.Query
{
    public  class GetFilmByPage:IRequest<PagedList<FilmBriefDto>>
    {
        public int pageNumber { get; init; } = 1;
        public int pageSize { get; init; } = 10;
    }
    public class GetFilmByPageHandler : IRequestHandler<GetFilmByPage, PagedList<FilmBriefDto>>
    {
        IApplicationDbContext _context;
        IMapper _mapper;
        public GetFilmByPageHandler(IApplicationDbContext _context, IMapper _mapper)
        {
            this._context = _context;
            this._mapper = _mapper;
        }

        public async Task<PagedList<FilmBriefDto>> Handle(GetFilmByPage request, CancellationToken cancellationToken)
        {
            return await _context.Films.OrderBy(t => t.Titre)
                .ProjectTo<FilmBriefDto>(_mapper.ConfigurationProvider)
                .PagedListAsync(request.pageNumber,request.pageSize);
        }
    }
}
