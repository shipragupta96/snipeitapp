using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using YourNamespace.Hooks;
using YourNamespace.Pages;
using Xunit;
using Microsoft.Playwright;

namespace YourNamespace.Steps
{
    [Binding]
    public class CreateMacBookProSteps
    {
        private readonly IPage _page;
        public string assetTag = "";

        public CreateMacBookProSteps()
        {
            _page = PlaywrightHooks.Page;
        }

        [Given(@"I am logged in as admin on the Snipe‑IT demo")]
        public async Task GivenILoggedIn()
        {
            var loginPage = new LoginPage(_page);
            await loginPage.LoginAsync("admin", "password");
        }

        [When(@"I create a '(.*)' asset with status ""(.*)"" checked out to a random user")]
        public async Task WhenICreateAsset(string model, string status)
        {
            // _userName = $"user{Guid.NewGuid():N}".Substring(0, 8);
            var createPage = new CreateAssetPage(_page);
            await createPage.CreateAssetAsync(model, status);
        }

        [Then(@"I should see the asset in the asset list")]
        public async Task ThenShouldSeeAsset()
        {
            var assetsPage = new AssetsPage(_page);
            assetTag = await assetsPage.assetTag();
            bool exists = await assetsPage.AssetExistsAsync(assetTag);
            Assert.True(exists, $"Expected asset model '{assetTag}' to be present in list.");
        }

        [When(@"I navigate to the asset details page")]
        public async Task WhenNavigateToDetails()
        {
            var assetsPage = new AssetsPage(_page);
            await assetsPage.OpenAssetDetailsAsync(assetTag);
        }

        [Then(@"the asset details '(.*)' with status ""(.*)"" is created successfully")]
        public async Task ThenValidateDetails(string model, string status)
        {
            var detailsPage = new AssetDetailsPage(_page);
            await detailsPage.ValidateAssetDetailsAsync(assetTag, model, status);
        }

        [Then(@"the ""(.*)"" tab shows the checkout event properly")]
        public async Task ThenHistoryShowsCheckout(String tab)
        {
            var detailsPage = new AssetDetailsPage(_page);
            await detailsPage.ValidateHistoryCheckoutAsync(tab, assetTag);
        }
    }
}
