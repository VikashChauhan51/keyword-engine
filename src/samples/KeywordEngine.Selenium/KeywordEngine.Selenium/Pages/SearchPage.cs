

using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KeywordEngine.Selenium.Pages;
public class SearchPage
{
    private readonly IWebDriver driver;
    private IWebElement searchTermInput => driver.FindElement(By.Id("sb_form_q"));
    public SearchPage(IWebDriver driver)
    {
        this.driver = driver;
    }

    public SearchPage Goto()
    {
        driver.Url = "https://www.bing.com";
        return this;
    }

    public SearchPage Search(string text)
    {
        searchTermInput.SendKeys(text);
        searchTermInput.SendKeys(Keys.Enter);

        return this;
    }
}
