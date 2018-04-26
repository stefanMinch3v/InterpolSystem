namespace InterpolSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Logger;
    using Services.Admin;
    using System;
    using System.Linq;

    public class LoggerController : BaseAdminController
    {
        private const int ValuesPerPage = 7;
        private readonly ILoggerService loggerService;
        private int currentPageSize = ValuesPerPage;

        public LoggerController(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public IActionResult All(string search, int page = 1)
        {
            var logs = this.loggerService.All();

            if (!string.IsNullOrWhiteSpace(search))
            {
                logs = logs.Where(l => l.Username.ToLower().Contains(search.ToLower()));
                this.currentPageSize = logs.Count();
            }

            if (page < 1)
            {
                page = 1;
            }

            logs = logs
                    .Skip((page - 1) * ValuesPerPage)
                    .Take(ValuesPerPage);

            return View(new LoggerPagingViewModel
            {
                Logs = logs,
                Search = search,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.loggerService.Total() / (double)currentPageSize)
            });
        }
    }
}
