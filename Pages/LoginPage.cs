using Microsoft.Playwright;
using System.Threading.Tasks;

namespace YourNamespace.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page) => _page = page;

        public async Task LoginAsync(string username, string password)
        {
            await _page.GotoAsync("https://demo.snipeitapp.com/login");
            await _page.FillAsync("input#username", username);
            await _page.FillAsync("input#password", password);
            await _page.ClickAsync("button[type=submit]");
            await _page.WaitForSelectorAsync("nav >> text=Assets");
        }
    }
}