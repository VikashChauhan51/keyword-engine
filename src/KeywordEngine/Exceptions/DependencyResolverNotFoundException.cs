using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordEngine.Exceptions;
public class DependencyResolverNotFoundException: Exception
{
    public DependencyResolverNotFoundException(string keyword) : base($"No dependency resolver available to create {keyword} keyword without default constructor.")
    {

    }
}
