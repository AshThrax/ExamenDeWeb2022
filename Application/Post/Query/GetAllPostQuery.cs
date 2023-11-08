using Application.Common.Interfaces;
using Application.Common.Mapping;
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
    public class GetAllPostQuery : IRequest<PostVm>
    {
       
    }
    public class GetAllPostQueryHandler : IRequestHandler<GetAllPostQuery,PostVm >
    {
        IApplicationDbContext _context;
        IMapper _mapper;
        public GetAllPostQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostVm> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
        {
           return new PostVm
            {
                Lists = await _context.Posts
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Titre)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
