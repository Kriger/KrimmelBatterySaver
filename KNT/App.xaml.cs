using System.Diagnostics;
using System.Windows;

namespace KNT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Get Reference to the current Process
            var thisProc = Process.GetCurrentProcess();
            // Check how many total processes have the same name as the current one
            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
            { 
                // If ther is more than one, than it is already running.
                MessageBox.Show(KNT.Properties.Resources.ErrorMessage, KNT.Properties.Resources.ErrorTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Current.Shutdown();
                return;
            }

            base.OnStartup(e);
        }
    }
}
