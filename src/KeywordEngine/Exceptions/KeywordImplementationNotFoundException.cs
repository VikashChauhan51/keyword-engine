using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordEngine.Exceptions
{
    public class KeywordImplementationNotFoundException: Exception
    {
        public KeywordImplementationNotFoundException(string keyword):base($"Keyword implementation for {keyword} was not found.")
        {

        }
    }
}
