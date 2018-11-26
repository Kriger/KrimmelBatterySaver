using System.Diagnostics;
using System.Windows;

namespace KBS
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

            string errorMessageValue;
            string errorTitleValue;

            if (KBS.Properties.Settings.Default.SelectedLanguage == KBS.Properties.ResourcesRus.LanguageRusText)
            {
                errorMessageValue = KBS.Properties.ResourcesRus.ErrorMessage;
                errorTitleValue = KBS.Properties.ResourcesRus.ErrorTitle;
            }
            else
            {
                errorMessageValue = KBS.Properties.ResourcesEng.ErrorMessage;
                errorTitleValue = KBS.Properties.ResourcesEng.ErrorTitle;
            }

            // Check how many total processes have the same name as the current one
            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
            {
                // If there is more than one, than it is already running.
                MessageBox.Show(errorMessageValue, errorTitleValue, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Current.Shutdown();
                return;
            }

            base.OnStartup(e);
        }
    }
}