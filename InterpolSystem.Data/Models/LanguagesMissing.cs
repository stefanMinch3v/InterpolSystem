namespace InterpolSystem.Data.Models
{
    public class LanguagesMissing
    {
        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public int IdentityParticularsMissingId { get; set; }

        public IdentityParticularsMissing IdentityParticularsMissing { get; set; }
    }
}
