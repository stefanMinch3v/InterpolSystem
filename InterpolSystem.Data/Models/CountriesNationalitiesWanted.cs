namespace InterpolSystem.Data.Models
{
    public class CountriesNationalitiesWanted
    {
        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int IdentityParticularsWantedId { get; set; }

        public IdentityParticularsWanted IdentityParticularsWanted { get; set; }
    }
}
