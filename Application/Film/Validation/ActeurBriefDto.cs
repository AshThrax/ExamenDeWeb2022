using Application.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film.Validation
{
    public class ActeurBriefDto:IMapFrom<Domain.Entities.Acteur>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
    }
}
