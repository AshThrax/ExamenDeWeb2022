using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
        {
            DbSet<Domain.Entities.Film> Films { get; set; }
            DbSet<Domain.Entities.Acteur> Acteurs { get; set; }
            DbSet<Domain.Entities.Post> Posts{ get; set; }
            DbSet<Domain.Entities.RePost> RePosts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
       
    }
    

}
