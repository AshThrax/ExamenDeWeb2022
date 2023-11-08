using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repost.Command
{
    public class CreateRepostCommand : IRequest<int>
    {
        public int PostId {get;set;}
        public string MyRepost {get;set;}
        public string Username { get; set; }
    }
    public class CreateRepostCommandHandler : IRequestHandler<CreateRepostCommand,int>
    {
        IApplicationDbContext _context;
        public CreateRepostCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<int> Handle(CreateRepostCommand request, CancellationToken cancellationToken)
        {
            var entity = new  Domain.Entities.RePost();
            entity.Postid = request.PostId;
            entity.Response = request.MyRepost;
            entity.Username=request.Username;
            _context.RePosts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
