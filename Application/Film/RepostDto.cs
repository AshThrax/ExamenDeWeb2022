using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film
{
    public class RePostDto :IMapFrom<Domain.Entities.RePost>
    {
        public int Id { get; set; }
        public string Username { get; set; }  
        public string Response { get; set; }
        public int Postid { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.RePost, RePostDto>();
        }
    }
}
