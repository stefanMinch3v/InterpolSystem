namespace InterpolSystem.Services
{
    public class ServiceConstants
    {
        public const string InvalidInsertedData = "Invalid data.";
        public const string InvalidInsertedPerson = "Invalid person.";

        public const string InvalidFormInfo = "Invalid submitted form.";

        public const string PdfCertificateFormat = @"
<h1 style = ""text-align:center""><u>Certificate proving carch of criminal</u></h1>
<br />
<h2 style = ""text-align:center"">To {0}</h2>
<br />
<h3 style = ""text-align:center"" >Date of issue: {1}</h2>
<h3 style = ""text-align:center"" >Date of submission {2}</h3>
<h4>This certificate proves catch of criminal named :
            <u>{4}</u> this criminal was escorted to {3}
        police department. In the name of Interpol we are kindly thanking you
            for your contribution.
        </h4>
<h6 style = ""text-align : right"">
        Thank you
        <br> Admiral of Interpoll
        <br> Tarim Abzhal
        <br>
    </h6>
";
    }
}
