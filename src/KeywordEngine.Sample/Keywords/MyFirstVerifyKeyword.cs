using System;
using KeywordEngine.Abstraction;
using System.Threading.Tasks;
using KeywordEngine.Models;

namespace KeywordEngine.Sample.Keywords
{
    internal class MyFirstVerifyKeyword : IVerifyKeyword
    {
        public MyFirstVerifyKeyword()
        {

        }


        public Task<KeywordResponse> Execute()
        {
            Console.WriteLine($"{nameof(MyFirstVerifyKeyword)} keyword executed.");

            return Task.FromResult(new KeywordResponse
            {
                Status = ResponseStatus.Executed,
                Message = $"{nameof(MyFirstVerifyKeyword)} keyword executed."
            });
        }
    }
}
