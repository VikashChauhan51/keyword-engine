using System;
using System.Collections.Generic;
using System.Linq;
using KeywordEngine.Abstraction;
using System.Threading.Tasks;

namespace KeywordEngine.Sample.Keywords
{
    internal class MyFirstVerifyKeyword: IVerifyKeyword
    {
        public MyFirstVerifyKeyword()
        {

        }
        public void Execute()
        {
            Console.WriteLine($"{nameof(MyFirstVerifyKeyword)} keyword executed.");
        }
    }
}
