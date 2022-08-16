using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.DebuggerVisualizers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Sylvite.Transport;
using Sylvite.Visualizer.Rebuilder;

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

        var transports = new List<SyntaxTransport>();

        while (true)
        {
            var getObjectRequest = new GetObjectRequest(2);
            var response = objectProvider.GetObject(getObjectRequest);

            transports.AddRange(response.Transports);

            if (response.Completed)
            {
                break;
            }
        }

        var rebuilder = new SyntaxRebuilder();
        var node = rebuilder.BuildNode(transports);

        this.Code = node.NormalizeWhitespace().ToFullString();
    }
}
