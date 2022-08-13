using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sylvite.Diagnostics;

public static class Guard
{
    [Conditional("DEBUG")]
    public static void NotNull<T>(
        T value,
        [CallerArgumentExpression("value")] string parameterName = "")
        where T :
            class
    {
        if (value is null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }
}
