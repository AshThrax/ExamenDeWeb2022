using Application.Common.Interfaces;
using Application.Film;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Post.Command
{
    public class CreatePostCommand : IRequest<int>
    {

        public string UserName { get; set; }
        public  string Titre{ get; set; }
        public string PostContent { get; set; }
    }
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand,int>
    {
        IApplicationDbContext _context;
        public CreatePostCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var entity= new Domain.Entities.Post();
            entity.UserName=request.UserName;
            entity.PostContent = request.PostContent;
            entity.Titre = request.Titre;
            _context.Posts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
