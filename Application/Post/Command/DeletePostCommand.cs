using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Post.Command
{
    public class DeletePostCommand:IRequest
    {
        public int Id { get; set; }
    
    }
    public class deletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        IApplicationDbContext _context;
        public deletePostCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            //verifier qque l'entité en question existe belle et bien
            var entity = await _context.Posts
                
                .FirstOrDefaultAsync(x =>x.Id ==request.Id);
            if (entity == null)
            {
                return default;
            }

            _context.Posts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
