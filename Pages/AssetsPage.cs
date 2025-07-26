using Microsoft.Playwright;
using System.Threading.Tasks;

namespace YourNamespace.Pages
{
    public class AssetsPage
    {
        private readonly IPage _page;

        public AssetsPage(IPage page) => _page = page;

        public async Task<bool> AssetExistsAsync(string searchTerm)
        {
            await _page.ClickAsync("nav >> text=Assets");
            await _page.FillAsync("input[type=search]", searchTerm);
            await _page.PressAsync("input[type=search]", "Enter");
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            var row = await _page.QuerySelectorAsync($"table >> text={searchTerm}");
            return row != null;
        }

        public async Task OpenAssetDetailsAsync(string searchTerm)
        {
            await _page.ClickAsync($"table >> text={searchTerm}");
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public async Task<string> assetTag()
        {
            var locator = _page.Locator("div.alert-success");
            string alertText = await locator.InnerTextAsync();
            string assetTag = alertText.Split(' ').FirstOrDefault(word => word.StartsWith("SA"));
            return assetTag;
        }

    }
}