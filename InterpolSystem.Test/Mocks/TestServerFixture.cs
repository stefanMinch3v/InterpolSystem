namespace InterpolSystem.Test.Mocks
{
    using InterpolSystem.Web;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.PlatformAbstractions;
    using System;
    using System.IO;
    using System.Net.Http;

    public class TestServerFixture : IDisposable
    {
        // private const string BaseUri = "https://localhost:4368/";
        private readonly TestServer testServer;

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                //.UseContentRoot(GetContentRootPath())
                .UseStartup<Startup>();

            testServer = new TestServer(builder);
            Client = testServer.CreateClient();
            //this.Client.BaseAddress = new Uri(BaseUri);
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            this.Client?.Dispose();
            testServer?.Dispose();
        }

        //private string GetContentRootPath()
        //{
        //    var testProjectPath = PlatformServices.Default.Application.ApplicationBasePath;
        //    var relativePathToHostProject = "../../../../InterpolSystem.Web";
        //    return Path.Combine(testProjectPath, relativePathToHostProject);
        //}
    }
}
