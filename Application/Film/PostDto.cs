using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film
{
    public class PostDto : IMapFrom<Domain.Entities.Post>
    {
        public PostDto()
        {
            RepostList = new List<RePostDto>();
        }

        public int Id { get; set;}
        public string Username { get; set;}
        public string Titre { get; set;}          
        public string PostContent { get; set; }
        
        public IList<RePostDto> RepostList {get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Post, PostDto>();
        }
    }
}
