namespace InterpolSystem.Data.Models
{
    public class CountriesNationalitiesMissing
    {
        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int IdentityParticularsMissingId { get; set; }

        public IdentityParticularsMissing IdentityParticularsMissing { get; set; }
    }
}
