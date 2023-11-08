using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Post.Command
{
    public class UpdatePostCommand:IRequest<int>
    {
        public int Id { get; set; }
        public string Titre { get; set; }
      
        public string PostContent { get; set; }
    }
    public class UpdatePostCommandhandler : IRequestHandler<UpdatePostCommand, int>
    {
        IApplicationDbContext _context;
        public UpdatePostCommandhandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var entity= _context.Posts
                .FirstOrDefault(x =>x.Id==request.Id);
            if (entity == null)
            {
                return default;
            }

            entity.Titre=request.Titre;
            entity.PostContent = request.PostContent;
            
            _context.Posts.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
            
        }
    }
}
