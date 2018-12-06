namespace InterpolSystem.Test.Web.Controllers
{
    using InterpolSystem.Test.Mocks;
    using System.Threading.Tasks;
    using Xunit;

    public class MissingPeopleControllerIntegrationTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture server;

        public MissingPeopleControllerIntegrationTests(TestServerFixture server)
        {
            this.server = server;
        }
        // TODO SERVER CANNOT FIND FOLDERS
        //[Fact]
        //public async Task IndexPageShouldReturnStatusCode200()
        //{
        //    var client = this.server.Client;
        //    var response = await client.GetAsync("/");

        //    response.EnsureSuccessStatusCode();
        //}

    }
}
