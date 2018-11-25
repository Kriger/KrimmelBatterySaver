using System.Windows;

namespace KBS
{
    public partial class Settings
    {
        private readonly string _highPerformance = Properties.Resources.HighPerformanceGUID;
        private readonly string _balanceMode = Properties.Resources.BalancedModeGUID;
        private readonly string _energySaving = Properties.Resources.EnergySavingGUID;

        public Settings()
        {
            InitializeComponent();

            cb_language.Items.Clear();
            cb_language.Items.Add(Properties.Resources.LanguageRusText);
            cb_language.Items.Add(Properties.Resources.LanguageEngText);
            cb_language.SelectedIndex = 
                Properties.Settings.Default.SelectedLanguage == Properties.Resources.LanguageRusText ? 0 : 1;

            cb_online.Items.Add(Properties.Resources.HighPerformanceTitle);
            cb_online.Items.Add(Properties.Resources.BalancedModeTitle);
            cb_online.SelectedIndex = 
                Properties.Settings.Default.OnlineModeGUID == new System.Guid(_highPerformance) ? 0 : 1;

            cb_offline.Items.Add(Properties.Resources.BalancedModeTitle);
            cb_offline.Items.Add(Properties.Resources.EnergySavingTitle);
            cb_offline.SelectedIndex = 
                Properties.Settings.Default.OfflineModeGUID == new System.Guid(_balanceMode) ? 0 : 1;
        }

        private void SaveSettings()
        {
            var onlineGuid = new System.Guid(cb_online.SelectedIndex == 0 ? _highPerformance : _balanceMode);
            var offlineGuid = new System.Guid(cb_online.SelectedIndex == 0 ? _energySaving : _balanceMode);

            Properties.Settings.Default["SelectedLanguage"] = cb_language.SelectedValue;
            Properties.Settings.Default["OnlineModeGUID"] = onlineGuid;
            Properties.Settings.Default["OfflineModeGUID"] = offlineGuid;
            Properties.Settings.Default.Save();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
