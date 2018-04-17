namespace InterpolSystem.Services.Models.WantedPeople
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;
    using System.Linq;

    public class WantedPeopleListingServiceModel : IMapFrom<IdentityParticularsWanted>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string GivenNationalities { get; set; }

        public string PictureUrl { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<IdentityParticularsWanted, WantedPeopleListingServiceModel>()
                .ForMember(mp => mp.GivenNationalities, cfg => cfg.MapFrom(ipm => ipm.Nationalities.Select(n => n.Country.Name).FirstOrDefault()))
                .ForMember(mp => mp.PictureUrl, cfg => cfg.MapFrom(ipm => ipm.PhysicalDescription.PictureUrl));
    }
}
