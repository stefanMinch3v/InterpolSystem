namespace InterpolSystem.Web.Models.WantedPeople
{
    using InterpolSystem.Services.Blog.Models;
    using Services.Models.WantedPeople;
    using Shared;
    using System.Collections.Generic;

    public class WantedPeoplePageListingModel : SearchFormViewModel
    {
        public IEnumerable<WantedPeopleListingServiceModel> WantedPeople { get; set; } = new List<WantedPeopleListingServiceModel>();

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

        public IEnumerable<ArticlesListingsServiceModel> Articles {get; set;}
    }
}
