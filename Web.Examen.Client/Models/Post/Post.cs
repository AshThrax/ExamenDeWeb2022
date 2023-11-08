using Web.Examen.Client.Models.RePosts;
namespace Web.Examen.Client.Models.Posts
{
    public class Post
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Titre { get; set; }
        public string PostContent { get; set; }

        public List<RePost> RepostList { get; set; }
    }
}
