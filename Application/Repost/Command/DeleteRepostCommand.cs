using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repost.Command
{
    public  class DeleteRepostCommand:IRequest
    {
       public int Id { get; set; }
       public int PostId { get; set; }
    }
    public class DeleteRepostCommandHandler : IRequestHandler<DeleteRepostCommand>
    {
        IApplicationDbContext _context;
        public DeleteRepostCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRepostCommand request, CancellationToken cancellationToken)
        {
            //verifier qque l'entité en question existe belle et bien
            var entity = await _context.RePosts
                .Where(c => c.Id == request.Id)
                .Where(c => c.Postid == request.PostId)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                return default;
            }
           
               _context.RePosts.Remove(entity);
               await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
