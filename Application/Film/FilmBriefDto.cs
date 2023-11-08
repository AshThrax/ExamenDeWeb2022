using Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film
{
    public class FilmBriefDto : IMapFrom<Domain.Entities.Film>
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Genre { get; set; }
    }
}
