namespace InterpolSystem.Web.Areas.BountyAdmin.Models.WantedPeople
{
    using InterpolSystem.Data.Models;
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    public class SubmitFormWantedViewModel
    {
        public int Id { get; set; }

        public int WantedId { get; set; }

        public string Message { get; set; }

        public string Subject { get; set; }

        public Byte[] Image { get; set; }

        public string PoliceDepartment { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<SubmitForm> SubmitForms { get; set; } = new List<SubmitForm>();

    }
}
