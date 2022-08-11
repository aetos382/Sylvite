using System;

using Microsoft.VisualStudio.DebuggerVisualizers;

namespace Sylvite.Visualizer;

public class SyntaxVisualizer :
    DialogDebuggerVisualizer
{
    protected override void Show(
        IDialogVisualizerService windowService,
        IVisualizerObjectProvider objectProvider)
    {
        if (objectProvider is null)
        {
            throw new ArgumentNullException(nameof(objectProvider));
        }

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

        window.ShowDialog();
    }
}
