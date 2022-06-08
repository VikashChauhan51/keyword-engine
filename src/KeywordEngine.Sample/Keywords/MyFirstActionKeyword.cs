using KeywordEngine.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordEngine.Sample.Keywords
{
    internal class MyFirstActionKeyword : IActionKeyword
    {
        private readonly string _message;
        public MyFirstActionKeyword( string message)
        {
            _message = message;
        }
        public void Execute()
        {
            Console.WriteLine(_message);
            Console.WriteLine($"{nameof(MyFirstActionKeyword)} keyword executed.");
        }
    }
}
