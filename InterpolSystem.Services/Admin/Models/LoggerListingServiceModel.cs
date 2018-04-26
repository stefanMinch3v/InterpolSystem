namespace InterpolSystem.Services.Admin.Models
{
    using Common.Mapping;
    using Data.Models;
    using System;

    public class LoggerListingServiceModel : IMapFrom<LogEmployee>
    {
        public DateTime Date { get; set; }

        public string IpAddress { get; set; }

        public string Username { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string ExceptionType { get; set; }

        public string ExceptionMessage { get; set; }
    }
}
