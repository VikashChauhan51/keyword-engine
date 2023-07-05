
namespace KeywordEngine.Playwright.Pages;
public class SearchPage : PageBase
{
    private readonly ILocator _searchTermInput;

    public SearchPage(IPage page) : base(page)
    {
        _searchTermInput = page.Locator("textarea[id='sb_form_q']");
    }

    public async Task Goto()
    {
        await page.GotoAsync("/");
    }

    public async Task Search(string text)
    {
        await _searchTermInput.FillAsync(text);
        await _searchTermInput.PressAsync("Enter");
    }
}
