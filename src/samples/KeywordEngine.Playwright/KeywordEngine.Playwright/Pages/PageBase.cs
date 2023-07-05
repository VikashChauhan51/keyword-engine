using NUnit.Framework.Internal;

namespace KeywordEngine.Playwright.Pages;
public abstract class PageBase : IDisposable, IAsyncDisposable
{

    protected readonly IPage page;
    public PageBase(IPage page)
    {
        this.page = page;

        this.page.Load += Page_Load;
        this.page.Close += Page_Close;
        this.page.Console += Page_Console;
        this.page.PageError += Page_PageError;
        this.page.Crash += Page_Crash;
    }

    private void Page_Crash(object? sender, IPage e)
    {
        Console.WriteLine($"Crashed page URL is {e.Url}");

         
    }

    private void Page_PageError(object? sender, string e)
    {
        Console.WriteLine(e);

    }

    private void Page_Console(object? sender, IConsoleMessage e)
    {
        Console.WriteLine(e.ToString());

    }

    private void Page_Load(object? sender, IPage e)
    {
        Console.WriteLine($"Loaded page URL is {e.Url}");

    }
    private void Page_Close(object? sender, IPage e)
    {
        Console.WriteLine($"Closed page URL is {e.Url}");

    }



    public void Dispose()
    {
        this.page.Load -= Page_Load;
        this.page.Close -= Page_Close;
        this.page.Console -= Page_Console;
        this.page.PageError -= Page_PageError;
        this.page.Crash -= Page_Crash;
        GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;

    }
}
