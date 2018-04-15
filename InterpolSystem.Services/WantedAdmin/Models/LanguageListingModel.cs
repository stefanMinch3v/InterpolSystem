﻿namespace InterpolSystem.Services.WantedAdmin.Models
{
    using Common.Mapping;
    using Data.Models;

    public class LanguageListingModel : IMapFrom<Language>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
