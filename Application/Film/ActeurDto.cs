using Application.Common.Mapping;
using Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film
{
    public class ActeurDto : IMapFrom<Domain.Entities.Acteur>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        public string Rolesdescription { get; set; }
        public int FilmId { get; set; }

        public void Mapping(Profile Profile)
        {
            Profile.CreateMap<Domain.Entities.Acteur, ActeurDto>();
        }

    }
}
