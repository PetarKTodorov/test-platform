namespace TestPlatform.Application.Infrastructures.Helpers.HTML
{
    using System;
    using System.Linq.Expressions;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class FormFieldHelper
    {
        public static IHtmlContent FormGroupFor<TModel, TResult>
            (this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            using var writer = new StringWriter();

            IHtmlContent label =
                htmlHelper.LabelFor(expression, new { @class = "form-label" });

            IHtmlContent input =
                htmlHelper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } });

            IHtmlContent validationMessage =
                htmlHelper.ValidationMessageFor(expression, null, new { @class = "text-danger" });

            writer.Write("<div class=\"mb-3\">");
            label.WriteTo(writer, HtmlEncoder.Default);
            input.WriteTo(writer, HtmlEncoder.Default);
            validationMessage.WriteTo(writer, HtmlEncoder.Default);
            writer.Write("</div>");

            return new HtmlString(writer.ToString());
        }
    }
}
