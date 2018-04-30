namespace InterpolSystem.Web.Infrastructure.Filters
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    using static WebConstants;

    public class SubmitFormAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                return;
            }

            var db = context
                .HttpContext
                .RequestServices
                .GetService<InterpolDbContext>();

            var unreadedForms = db.SubmitForms
                .Where(f => f.Status == 0)
                .ToList();

            if (!unreadedForms.Any())
            {
                return;
            }

            var controller = context.Controller as Controller;

            if (controller == null)
            {
                return;
            }

            controller.ViewData[ValidForm] = unreadedForms.Count;

            base.OnActionExecuting(context);
        }
    }
}
