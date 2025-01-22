using System;
using KeywordEngine.Test.Helpers;

namespace KeywordEngine.Test.Keywords;
public class DependencyInjectionActionKeyword: IActionKeyword
{
    private readonly IFooService fooService;
    public DependencyInjectionActionKeyword(IFooService fooService)
    {
        this.fooService = fooService;
    }

    public Task<KeywordResponse> ExecuteAsync()
    {
        fooService.DoThing(nameof(DependencyInjectionActionKeyword));

        return Task.FromResult(new KeywordResponse
        {
            Status = ResponseStatus.Executed,
            Message = $"{nameof(DependencyInjectionActionKeyword)} keyword executed."
        });
    }
}
