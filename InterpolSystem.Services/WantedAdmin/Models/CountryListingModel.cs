namespace InterpolSystem.Services.WantedAdmin.Models
{
    using Common.Mapping;
    using Data.Models;

    public class CountryListingModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
