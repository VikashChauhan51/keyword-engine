using System;
using System.Threading.Tasks;
using KeywordEngine.Abstraction;
using KeywordEngine.Models;
using KeywordEngine.Selenium.Pages;
using OpenQA.Selenium;

namespace KeywordEngine.Selenium.Keywords;
public class SearchKeyword : IActionKeyword
{
    private readonly IWebDriver webDriver;
    private readonly string text;
    public SearchKeyword(string text, ITestContext testContext)
    {
        this.webDriver = testContext?.Data[nameof(IWebDriver)] as IWebDriver ?? throw new ArgumentNullException(nameof(IWebDriver));
        this.text = text;
    }

    public Task<KeywordResponse> Execute()
    {
        new SearchPage(webDriver)
        .Goto()
       .Search(text);

        return Task.FromResult(new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(SearchKeyword)} keyword executed."
        });

    }
}
