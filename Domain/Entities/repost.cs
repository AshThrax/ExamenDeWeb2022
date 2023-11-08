using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class RePost
    {
        public int Id { get; set; }
        public string Username { get; set; }
        
        public string Response { get; set; }
        public int Postid { get; set; }
        public Post Post { get; set; }
    }
}
