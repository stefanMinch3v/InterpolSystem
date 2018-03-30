namespace InterpolSystem.Services.Admin
{
    using Models;
    using System.Collections.Generic;

    public interface IAdminUserService
    {
        IEnumerable<AdminUserListingServiceModel> All();
    }
}
