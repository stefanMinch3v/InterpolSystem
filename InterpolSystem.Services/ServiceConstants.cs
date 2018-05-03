namespace InterpolSystem.Services
{
    public class ServiceConstants
    {
        public const string InvalidInsertedData = "Invalid data.";
        public const string InvalidInsertedPerson = "Invalid person.";

        public const string InvalidFormInfo = "Invalid submitted form.";

        public const string PdfCertificateFormat = @"
<h1>Cerificate</h1>
<h2>To {0}</h2>
<br />
<h2>Date of issued: {1}</h2>
<h3>Date of submission {2}</h3>
<h4>By police department {3}</h4>
<h5>Wanted person names: {4}</h5>
";
    }
}
