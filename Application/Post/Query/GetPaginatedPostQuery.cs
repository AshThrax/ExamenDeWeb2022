using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Film;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Post.Query
{
    public class GetPaginatedPostQuery : IRequest<PagedList<PostDto>>
    {
       
     
        public int PageNumber { get; init; } = 5;
        public int PageSize { get; init; } = 10;
    }
    public class GetPaginatedPostQueryHandler : IRequestHandler<GetPaginatedPostQuery, PagedList<PostDto> >
    {
        IApplicationDbContext _context;
        IMapper _mapper;
        public GetPaginatedPostQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<PostDto>> Handle(GetPaginatedPostQuery request, CancellationToken cancellationToken)
        {
            return await _context.Posts.ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .PagedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
