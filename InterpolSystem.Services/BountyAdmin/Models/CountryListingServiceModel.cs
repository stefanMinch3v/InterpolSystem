namespace InterpolSystem.Services.BountyAdmin.Models
{
    using Common.Mapping;
    using Data.Models;

    public class CountryListingServiceModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
