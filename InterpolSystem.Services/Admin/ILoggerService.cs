namespace InterpolSystem.Services.Admin
{
    using Services.Admin.Models;
    using System.Collections.Generic;

    public interface ILoggerService
    {
        IEnumerable<LoggerListingServiceModel> All();

        int Total();
    }
}
