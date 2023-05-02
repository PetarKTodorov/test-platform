namespace TestPlatform.Application.Infrastructures.Filtres
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        private const string FILTER_MODEL = "model";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                object model = context.ActionArguments
                    .FirstOrDefault(arg => arg.Key.ToLower().Contains(FILTER_MODEL))
                    .Value;

                Controller controller = context.Controller as Controller;
                if (controller != null)
                {
                    ViewResult view = controller.View(model);

                    context.Result = view;
                }
            }
        }
    }
}
