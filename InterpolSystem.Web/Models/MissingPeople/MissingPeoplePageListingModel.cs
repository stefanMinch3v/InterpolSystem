namespace InterpolSystem.Web.Models.MissingPeople
{
    using InterpolSystem.Services.Blog.Models;
    using Services.Models.MissingPeople;
    using Shared;
    using System.Collections.Generic;

    public class MissingPeoplePageListingModel : SearchFormViewModel
    {
        public IEnumerable<MissingPeopleListingServiceModel> MissingPeople { get; set; } = new List<MissingPeopleListingServiceModel>();

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

        public IEnumerable<ArticlesListingsServiceModel> Articles { get; set; }

    }
}
