namespace InterpolSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Language
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(LanguageMaxLength)]
        [MinLength(LanguageMinLength)]
        public string Name { get; set; }

        public List<LanguagesMissing> MissingPeopleLanguages { get; set; } = new List<LanguagesMissing>();

        public List<LanguagesWanted> WantedPeopleLanguages { get; set; } = new List<LanguagesWanted>();
    }
}
