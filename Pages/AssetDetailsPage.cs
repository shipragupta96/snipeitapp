using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

namespace YourNamespace.Pages
{
    public class AssetDetailsPage
    {
        private readonly IPage _page;

        public AssetDetailsPage(IPage page) => _page = page;

        public async Task ValidateAssetDetailsAsync(string assetTag, string model, string status)
        {
            var actualAssetTag = (await _page.TextContentAsync("span[class*=assettag]")).Trim();
            var actualStatus = (await _page.TextContentAsync("section#main a[href*=statuslabels]")).Trim();
            var actualModel = (await _page.TextContentAsync("div.row-new-striped a[href*=models]")).Trim();

            Assert.Equal(model, actualModel);
            Assert.Equal(status, actualStatus);
            Assert.Equal(assetTag, actualAssetTag);
        }

        public async Task ValidateHistoryCheckoutAsync(string tab, string assetTag)
        {
            await _page.ClickAsync("a[href='#" + tab + "']");
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            string row = (await _page.TextContentAsync("a[href*=hardware][data-original-title=asset]")).Trim();
            Assert.True(row.Contains(assetTag));
        }
    }
}
