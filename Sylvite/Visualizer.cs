using Microsoft.VisualStudio.DebuggerVisualizers;

namespace Sylvite;

public class Visualizer :
    DialogDebuggerVisualizer
{
    protected override void Show(
        IDialogVisualizerService windowService,
        IVisualizerObjectProvider objectProvider)
    {
        var window = new MainWindow();
        window.ShowDialog();
    }
}
