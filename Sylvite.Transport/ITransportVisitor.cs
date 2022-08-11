namespace Sylvite.Transport;

public interface ITransportVisitor<out T>
{
    T VisitCompilationUnit(
        CompilationUnitTransport transport);

    T VisitNamespaceDeclaration(
        NamespaceDeclarationTransport transport);

    T VisitFileScopedNamespaceDeclaration(
        FileScopedNamespaceDeclarationTransport transport);

    T VisitClassDeclaration(
        ClassDeclarationTransport transport);
}
