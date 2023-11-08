using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repost.Command
{
    public class UpdateRepostCommand:IRequest<int>
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string MyRepost { get; set; }
       

    }
    public class UpdateRepostCommandHandler : IRequestHandler<UpdateRepostCommand, int>
    {
        IApplicationDbContext _context;
        public UpdateRepostCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateRepostCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.RePosts.Where(x => x.Postid == request.PostId).FirstOrDefaultAsync(x => x.Id == request.Id).Result;
            if(entity == null)
            {
                return default;
            }
            entity.Response=request.MyRepost;

            _context.RePosts.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
