namespace InterpolSystem.Services.Html.Implementations
{
    using Ganss.XSS;

    public class HtmlService : IHtmlService
    {
        private readonly IHtmlSanitizer htmlSanitizer;

        public HtmlService()
        {
            this.htmlSanitizer = new HtmlSanitizer();
            this.htmlSanitizer.AllowedAttributes.Add("class");
        }

        public string Sanitize(string htmlContent)
            => this.htmlSanitizer.Sanitize(htmlContent);
    }
}
