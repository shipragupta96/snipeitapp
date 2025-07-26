using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace YourNamespace.Hooks
{
    [Binding]
    public sealed class PlaywrightHooks
    {
        private static IBrowser? _browser;
        [ThreadStatic] public static IBrowserContext? BrowserContext;
        [ThreadStatic] public static IPage? Page;

        [BeforeTestRun]
        public static async Task BeforeTestRunAsync()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = true
            });
        }

        [BeforeScenario]
        public async Task BeforeScenarioAsync()
        {
            BrowserContext = await _browser.NewContextAsync();
            Page = await BrowserContext.NewPageAsync();
        }

        [AfterScenario]
        public async Task AfterScenarioAsync()
        {
            await BrowserContext.CloseAsync();
        }

        [AfterTestRun]
        public static async Task AfterTestRunAsync()
        {
            if (_browser is not null)
            {
                await _browser.CloseAsync();
            }
        }
    }
}