
using KeywordEngine.Abstraction;
using KeywordEngine.Models;
using KeywordEngine.Playwright.Pages;

namespace KeywordEngine.Playwright.Keywords;
public class SearchKeyword : IActionKeyword
{
    private readonly IPage page;
    private readonly string text;

    public SearchKeyword(string text, ITestContext testContext)
    {
        this.page = testContext?.Data[nameof(IPage)] as IPage ?? throw new ArgumentNullException(nameof(IPage));
        this.text = text;
    }

    public async Task<KeywordResponse> ExecuteAsync()
    {
        var searchPage = new SearchPage(page);
        await searchPage.Goto();
        await searchPage.Search(text);

        return new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(SearchKeyword)} keyword executed."
        };

    }
}
