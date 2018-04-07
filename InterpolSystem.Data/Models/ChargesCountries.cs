namespace InterpolSystem.Data.Models
{
    public class ChargesCountries
    {
        public int ChargesId { get; set; }

        public Charges Charges { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
