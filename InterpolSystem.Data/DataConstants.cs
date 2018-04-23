namespace InterpolSystem.Data
{
    public class DataConstants
    {
        public const int ChargesDescriptionMinLength = 5;
        public const int ChargesDescriptionMaxLength = 100;

        public const int ContinentNameMaxLength = 20;
        public const int ContinentNameMinLength = 4;
        public const int ContinentCodeMaxMinLength = 2;

        public const int CountryCodeMinMaxLength = 2;
        public const int CountryNameMaxLength = 50;
        public const int CountryNameMinLength = 3;

        public const int IdentityMissingNamesMinLength = 2;
        public const int IdentityMissingNamesMaxLength = 100;
        public const int IdentityMissingPlaceOfBirthMaxLength = 50;
        public const int IdentityMissingPlaceOfBirthMinLength = 3;
        public const int IdentityMissingPlaceOfDisappearanceMaxLength = 100;
        public const int IdentityMissingPlaceOfDisappearanceMinLength = 3;

        public const int IdentityWantedNamesMinLength = 2;
        public const int IdentityWantedNamesMaxLength = 100;
        public const int IdentityWantedPlaceOfBirthMaxLength = 50;
        public const int IdentityWantedPlaceOfBirthMinLength = 3;

        public const int LanguageMaxLength = 50;
        public const int LanguageMinLength = 2;

        public const int PhysicalDescriptionScarsOrMarksMaxLength = 100;
        public const int PhysicalDescriptionPcitureMaxLength = 2000;
        public const int PhysicalDescriptionPcitureMinLength = 10;

        public const int UserNamesMaxLength = 50;
        public const int UserNamesMinLength = 2;

        public const int ArticlesTitleMaxLength = 30;
        public const int ArticlesTitleMinLength = 3;
        public const int ArticlesContentMaxLength = 5000;
        public const int ArticlesContentMinLength = 5;

        public const string DateFormatString = "{0:dd-MM-yyyy}";

        public const string InvalidDateInThePast = "01/01/1800";

        public const int MessageMaxLenght = 500;
        public const int MessageMinLenght = 5;

        public const int SubjectMaxLength = 30;
        public const int SubjectMinLength = 5;

        public const int ImageMaxSize = 2 * 1024 * 1024; // 2mb
    }
}
