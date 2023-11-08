using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string UserName { get; set; } 
        public string Titre { get; set; }
        public string PostContent { get; set; }   
        public IList<RePost> RepostList { get; set; } = new List<RePost>();
    }
}
