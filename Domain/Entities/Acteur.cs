using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Acteur
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        public string Rolesdescription { get; set; }
        public int FilmId { get; set; }
        public Film Filmlist { get; set; } 
    }
}
