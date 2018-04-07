namespace InterpolSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Countinent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ContinentCodeMaxMinLength)]
        [MinLength(ContinentCodeMaxMinLength)]
        public string Code { get; set; }

        [Required]
        [MaxLength(ContinentNameMaxLength)]
        [MinLength(ContinentNameMinLength)]
        public string Name { get; set; }

        public List<Country> Countries { get; set; } = new List<Country>();
    }
}
