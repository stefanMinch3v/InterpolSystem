namespace InterpolSystem.Api.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;

    public class ValidateUrlIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;

            if (controller == null)
            {
                return;
            }

            var model = context
                .ActionArguments
                .FirstOrDefault(a => a.Key.Contains("id") || a.Key.Contains("page"))
                .Value;

            if (model == null)
            {
                context.Result = controller.BadRequest();
                return;
            }

            if (int.Parse(model.ToString()) <= 0)
            {
                context.Result = controller.BadRequest();
            }

            base.OnActionExecuting(context);
        }
    }
}
