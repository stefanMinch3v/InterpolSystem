namespace InterpolSystem.Services.BountyHunter
{
    using Models;
    using System.Collections.Generic;

    public interface IBountyHunterService
    {
        byte[] GetPdfCertificate(int wantedPersonId, string firstLastName, string hunterEmail);

        IEnumerable<HunterSubmittedFormsServiceModel> GetSubmittedForms(string currentUserId);
    }
}
