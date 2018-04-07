namespace InterpolSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CountryCodeMinMaxLength)]
        [MinLength(CountryCodeMinMaxLength)]
        public string Code { get; set; } 

        [Required]
        [MaxLength(CountryNameMaxLength)]
        [MinLength(CountryNameMinLength)]
        public string Name { get; set; }

        public int CountinentId { get; set; }

        public Countinent Countinent { get; set; }

        public List<CountriesNationalitiesMissing> NationalitiesMissingPeople { get; set; } = new List<CountriesNationalitiesMissing>();

        public List<CountriesNationalitiesWanted> NationalitiesWantedPeople { get; set; } = new List<CountriesNationalitiesWanted>();

        public List<ChargesCountries> ChargesCountries { get; set; } = new List<ChargesCountries>();
    }
}
