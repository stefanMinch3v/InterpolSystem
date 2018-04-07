namespace InterpolSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Charges
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ChargesDescriptionMaxLength)]
        [MinLength(ChargesDescriptionMinLength)]
        public string Description { get; set; }

        public int IdentityParticularsWantedId { get; set; }

        public IdentityParticularsWanted IdentityParticularsWanted { get; set; }

        public List<ChargesCountries> CountryWantedAuthorities { get; set; } = new List<ChargesCountries>();
    }
}
