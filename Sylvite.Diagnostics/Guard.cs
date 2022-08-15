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
    {
        if (value is null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    [Conditional("DEBUG")]
    public static void InRange<T>(
        T value,
        T min,
        T max,
        [CallerArgumentExpression("value")] string parameterName = "")
        where T :
            IComparable<T>
    {
        if (value.CompareTo(min) < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }

        if (value.CompareTo(max) > 0)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }
    }

    [Conditional("DEBUG")]
    public static void GreaterThanOrEqualTo<T>(
        T value,
        T min,
        [CallerArgumentExpression("value")] string parameterName = "")
        where T :
            IComparable<T>
    {
        if (value.CompareTo(min) < 0)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }
    }

    [Conditional("DEBUG")]
    public static void LessThanOrEqualTo<T>(
        T value,
        T max,
        [CallerArgumentExpression("value")] string parameterName = "")
        where T :
            IComparable<T>
    {
        if (value.CompareTo(max) > 0)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }
    }

    [Conditional("DEBUG")]
    public static void GreaterThan<T>(
        T value,
        T min,
        [CallerArgumentExpression("value")] string parameterName = "")
        where T :
            IComparable<T>
    {
        if (value.CompareTo(min) <= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }
    }

    [Conditional("DEBUG")]
    public static void LessThan<T>(
        T value,
        T max,
        [CallerArgumentExpression("value")] string parameterName = "")
        where T :
            IComparable<T>
    {
        if (value.CompareTo(max) >= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }
    }
}
