using System;

using Microsoft.VisualStudio.DebuggerVisualizers;

using CommunityToolkit.Diagnostics;

namespace Sylvite.Visualizer;

public class SyntaxVisualizer :
    DialogDebuggerVisualizer
{
    protected override void Show(
        IDialogVisualizerService windowService,
        IVisualizerObjectProvider objectProvider)
    {
        Guard.IsNotNull(windowService);
        Guard.IsNotNull(objectProvider);

        if (objectProvider is not IAsyncVisualizerObjectProvider provider)
        {
            throw new NotSupportedException();
        }

        var window = new MainWindow
        {
            DataContext = new ViewModel
            {
                ObjectProvider = provider
            }
        };

        if (windowService is WinForms::IWin32Window wnd)
        {
            window.SetOwner(wnd);
        }

        _ = window.ShowDialog();
    }
}
