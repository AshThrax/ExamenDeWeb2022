using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Film
    {
        public int Id { get; set; }
        public string? Titre { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public IList<Acteur> Acteurs { get; set; } = new List<Acteur>();

       

    }
}
