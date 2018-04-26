namespace InterpolSystem.Web.Infrastructure.Filters
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class LogEmployeesAttribute : ActionFilterAttribute
    {
        private const string Path = @"D:\Programming\UCN\Fourth semester\ProjectGit\InterpolSystem\InterpolSystem.Web\logs.txt";

        private DateTime SpecificDate { get; set; } = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 03, 0, 0, 0);

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Task.Run(async () =>
            {
                if (!context.ModelState.IsValid)
                {
                    return;
                }

                var date = DateTime.UtcNow;
                var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
                var user = context.HttpContext.User.Identity.Name;
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];

                var saveInformation = $"{date}-{ipAddress}-{user}-{controller}-{action}-";

                if (context.Exception != null)
                {
                    var exceptionType = context.Exception.GetType().Name;
                    var exceptionMessage = context.Exception.Message;

                    saveInformation += $"{exceptionType}-{exceptionMessage}";
                }

                using (var writer = new StreamWriter("logs.txt", true))
                {
                    await writer.WriteLineAsync(saveInformation);
                }

                if (date >= this.SpecificDate)
                {
                    this.SpecificDate = this.SpecificDate.AddDays(1);
                    await SaveDataToDb(context);
                    File.Delete(Path);
                }
            })
            .GetAwaiter()
            .GetResult();
     
            base.OnActionExecuted(context);
        }

        public async Task SaveDataToDb(ActionExecutedContext context)
        {
            var list = new List<string>();

            if (!File.Exists(Path))
            {
                return;
            }

            using (var reader = new StreamReader(Path))
            {
                while (!reader.EndOfStream)
                {
                    var lineFromDocument = await reader.ReadLineAsync();
                    list.Add(lineFromDocument);
                }
            }

            if (list.Count == 0)
            {
                return;
            }

            var db = context
                .HttpContext
                .RequestServices
                .GetService<InterpolDbContext>();

            foreach (var line in list)
            {
                var data = line.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                var date = DateTime.Parse(data[0]);
                var ipAddress = data[1];
                var username = data[2];
                var controller = data[3];
                var action = data[4];

                var logger = new LogEmployee
                {
                    Date = date,
                    IpAddress = ipAddress,
                    Username = username,
                    ControllerName = controller,
                    ActionName = action
                };

                if (data.Length > 5)
                {
                    var exceptionType = data[5];
                    var exceptionMessage = data[6];

                    logger.ExceptionType = exceptionType;
                    logger.ExceptionMessage = exceptionMessage;
                }

                db.LogEmployees.Add(logger);
            }

            await db.SaveChangesAsync();
        }
    }
}
