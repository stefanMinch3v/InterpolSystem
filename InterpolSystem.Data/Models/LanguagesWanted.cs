namespace InterpolSystem.Data.Models
{
    public class LanguagesWanted
    {
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public int IdentityParticularsWantedId { get; set; }

        public IdentityParticularsWanted IdentityParticularsWanted { get; set; }
    }
}
