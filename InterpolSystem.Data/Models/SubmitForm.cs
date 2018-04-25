namespace InterpolSystem.Data.Models
{
    using Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class SubmitForm
    {
        public int Id { get; set; }

        public int? IdentityParticularsMissingId { get; set; }

        public IdentityParticularsMissing MissingPerson { get; set; }

        public int? IdentityParticularsWantedId { get; set; }

        public IdentityParticularsWanted WantedPerson { get; set; }

        public string PoliceDepartment { get; set; }
        
        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [Required]
        [MaxLength(SubjectMaxLength)]
        [MinLength(SubjectMinLength)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(MessageMaxLenght)]
        [MinLength(MessageMinLenght)]
        public string Message { set; get; }

        [MaxLength(ImageMaxSize)]
        public byte[] PersonImage { get; set; }

        public DateTime SubmissionDate { get; set; }

        public FormOptions Status { get; set; }
    }
}
