using Microsoft.Playwright;
using System.Threading.Tasks;

namespace YourNamespace.Pages
{
    public class CreateAssetPage
    {
        private readonly IPage _page;

        public CreateAssetPage(IPage page) => _page = page;

        public async Task CreateAssetAsync(string modelName, string statusText)
        {
            await _page.ClickAsync("text=Create New");
            await _page.Locator("a[href*='/hardware/create']").Filter(new() { HasTextString = "Asset" }).ClickAsync();
            await _page.ClickAsync("span#select2-company_select-container");
            await _page.Locator("ul#select2-company_select-results >> text=Hahn LLC").ClickAsync();
            await _page.ClickAsync("span#select2-model_select_id-container");
            await _page.Locator("ul#select2-model_select_id-results li").Filter(new() { HasTextString = modelName }).ClickAsync();
            await _page.ClickAsync("span#select2-status_select_id-container");
            await _page.Locator("ul#select2-status_select_id-results li").Filter(new() { HasTextString = statusText }).ClickAsync();
            // await _page.FillAsync("input[placeholder='User']", userName);
            await _page.ClickAsync("button#submit_button");
            await _page.WaitForSelectorAsync("div.alert-success");
        }
    }
}