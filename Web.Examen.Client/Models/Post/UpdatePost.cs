using Web.Examen.Client.Models.RePosts;

namespace Web.Examen.Client.Models.Posts
{
    public class UpdatePost
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Titre { get; set; }
        public string PostContent { get; set; }

        public List<RePost> MyReposts { get; set; }
    }
}
