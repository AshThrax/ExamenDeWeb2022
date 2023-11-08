using Application.Common.Interfaces;
using Application.Film;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Post.Query
{
    public class GetPostQuery:IRequest<PostDto>
    {
      public int Id { get; set; }

    }
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostDto>
    {
        IApplicationDbContext _context;
        IMapper _mapper;
        public GetPostQueryHandler(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            return await _context.Posts.ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == request.Id);
        }
    }
}
