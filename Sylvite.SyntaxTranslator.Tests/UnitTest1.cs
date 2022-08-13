using System;

using Microsoft.CodeAnalysis.CSharp;

using Xunit;

using Sylvite.Transport;

namespace Sylvite.SyntaxTranslator.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        const string code = @"
namespace Foo
{
    class Bar
    {
    }

    class Baz
    {
    }

    class Quux
    {
        int Field1 = 1;
        int Field2 = 2;
    }
}";
        var tree = CSharpSyntaxTree.ParseText(code);

        var converter = new SyntaxConverter();

        var observer = new Observer();
        using var subscription = converter.Subscribe(observer);

        var transport = converter.Visit(tree.GetCompilationUnitRoot());
    }

    private class Observer :
        IObserver<SyntaxTransport>
    {
        public void OnCompleted()
        {
        }

        public void OnError(
            Exception error)
        {
        }

        public void OnNext(
            SyntaxTransport value)
        {
        }
    }
}
