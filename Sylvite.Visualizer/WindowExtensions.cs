using System.Windows;
using System.Windows.Interop;

namespace Sylvite.Visualizer;

internal static class WindowExtensions
{
    public static void SetOwner(
        this Window window,
        WinForms::IWin32Window owner)
    {
        new WindowInteropHelper(window).Owner = owner.Handle;
    }
}
