using System.Linq;

using Microsoft.CodeAnalysis.CSharp;

using Xunit;

namespace Sylvite.SyntaxTranslator.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        const string code = @"
namespace Foo
{
    [A]
    class Bar
    {
        int Z { get { return 1; } }
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

        var converter = new SyntaxConverter(tree.GetCompilationUnitRoot());
        var array = converter.ToArray();
    }
}
