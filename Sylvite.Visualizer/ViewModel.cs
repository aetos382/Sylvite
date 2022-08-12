using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.DebuggerVisualizers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Sylvite.Transport;

namespace Sylvite.Visualizer;

[INotifyPropertyChanged]
public partial class ViewModel
{
    internal IAsyncVisualizerObjectProvider? ObjectProvider { get; init; }

    [ObservableProperty]
    private string? _code;

    [RelayCommand]
    private void Loaded()
    {
        var objectProvider = this.ObjectProvider;

        if (objectProvider is null)
        {
            return;
        }

        var transport = objectProvider
            .GetDeserializableObject()
            .ToObject<SyntaxTransport>();

        var rebuilder = new SyntaxRebuilder();

        var code = rebuilder
            .BuildSyntax(transport)
            .NormalizeWhitespace()
            .ToFullString();

        this.Code = code;
    }
}
