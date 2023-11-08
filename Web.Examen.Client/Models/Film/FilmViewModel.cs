using Web.Examen.Client.Models.Acteur;

namespace Web.Examen.Client.Models.Film
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }

        public List<ActeurViewModel> Acteurs { get; set; }
    }
}
