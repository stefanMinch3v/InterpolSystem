namespace InterpolSystem.Data.Models
{
    using Enums;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class PhysicalDescription
    {
        public int Id { get; set; }

        [Range(0, double.MaxValue)]
        public double Height { get; set; }

        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        public Color HairColor { get; set; }

        public Color EyeColor { get; set; }

        [Required]
        [MaxLength(PhysicalDescriptionPcitureMaxLength)]
        [MinLength(PhysicalDescriptionPcitureMinLength)]
        [RegularExpression(@"^(http|https):\/\/.*$")]
        public string PictureUrl { get; set; }

        [MaxLength(PhysicalDescriptionScarsOrMarksMaxLength)]
        public string ScarsOrDistinguishingMarks { get; set; }
    }
}
