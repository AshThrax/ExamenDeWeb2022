namespace Web.Examen.Client.Models.Film
{
    public class SaveFilm
    {
        public int Id { get; set; }
        public string Titre { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
        public string Genre { get; set; }
    }
}
