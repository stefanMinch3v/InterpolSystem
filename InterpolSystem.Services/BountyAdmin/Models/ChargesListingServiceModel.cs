namespace InterpolSystem.Services.BountyAdmin.Models
{
    using Common.Mapping;
    using Data.Models;

    public class ChargesListingServiceModel : IMapFrom<Charges>
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}
