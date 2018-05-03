namespace InterpolSystem.Services.BountyHunter.Models
{
    using Common.Mapping;
    using Data.Models;
    using Data.Models.Enums;
    using System;

    public class HunterSubmittedFormsServiceModel : IMapFrom<SubmitForm>
    {
        public int? IdentityParticularsWantedId { get; set; }

        public string PoliceDepartment { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime SubmissionDate { get; set; }

        public FormOptions Status { get; set; }
    }
}
