namespace InterpolSystem.Services.BountyHunter.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models.Enums;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static ServiceConstants;

    public class BountyHunterService : IBountyHunterService
    {
        private readonly InterpolDbContext db;
        private readonly IPdfGenerator pdfGenerator;

        public BountyHunterService(
            InterpolDbContext db,
            IPdfGenerator pdfGenerator)
        {
            this.db = db;
            this.pdfGenerator = pdfGenerator;
        }

        public byte[] GetPdfCertificate(int wantedPersonId, string firstLastName, string hunterEmail)
        {
            var certificateInfo = this.db.SubmitForms
                .Where(f => f.IdentityParticularsWantedId == wantedPersonId
                && f.SenderEmail == hunterEmail
                && f.Status == FormOptions.Accepted)
                .Select(f => new
                {
                    HunterNames = firstLastName,
                    DateOfSubmission = f.SubmissionDate,
                    DateOfIssued = DateTime.UtcNow,
                    ByPoliceDepartment = f.PoliceDepartment,
                    CoughtPersonNames = f.WantedPerson.FirstName + " " + f.WantedPerson.LastName
                })
                .FirstOrDefault();
                
            return this.pdfGenerator.GeneratePdfFromHtml(
                string.Format(
                    PdfCertificateFormat,
                    certificateInfo.HunterNames,
                    certificateInfo.DateOfIssued,
                    certificateInfo.DateOfSubmission,
                    certificateInfo.ByPoliceDepartment,
                    certificateInfo.CoughtPersonNames));
        }

        public IEnumerable<HunterSubmittedFormsServiceModel> GetSubmittedForms(string currentUserId)
             => this.db.SubmitForms
                .Where(f => f.UserId == currentUserId)
                .ProjectTo<HunterSubmittedFormsServiceModel>()
                .ToList();
    }
}
