namespace InterpolSystem.Services.BountyAdmin.Models
{
    using Common.Mapping;
    using Data.Models;
    using Data.Models.Enums;
    using System;

    public class SubmitFormWantedServiceModel : IMapFrom<SubmitForm>
    {
        public int Id { get; set; }

        public int? IdentityParticularsWantedId { get; set; }

        public string PoliceDepartment { get; set; }

        public string SenderEmail { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public byte[] PersonImage { get; set; }

        public DateTime SubmissionDate { get; set; }

        public FormOptions Status { get; set; }
    }
}
