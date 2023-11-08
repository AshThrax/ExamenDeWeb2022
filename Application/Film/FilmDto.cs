using Application.Common.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Film
{
    public class FilmDto : IMapFrom<Domain.Entities.Film>
    {
        public FilmDto()
        {
            Acteurs = new List<ActeurDto>();
        }
        public int Id { get; set; }
        public string Titre { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
        public string Genre { get; set; }

        public IList<ActeurDto> Acteurs { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Film, FilmDto>();
        }

    }
}
