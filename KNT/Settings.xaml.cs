using System.Windows;

namespace KNT
{
    public partial class Settings
    {
        private string highPerformance = Properties.Resources.HighPerformanceGUID;
        private string balanceMode = Properties.Resources.BalancedModeGUID;
        private string energySaving = Properties.Resources.EnergySavingGUID;

        public Settings()
        {
            InitializeComponent();

            cb_language.Items.Clear();
            cb_language.Items.Add("Русский");
            cb_language.Items.Add("English");
            cb_language.SelectedIndex = 0;

            cb_online.Items.Add(Properties.Resources.HighPerformanceTitle);
            cb_online.Items.Add(Properties.Resources.BalancedModeTitle);
            cb_online.SelectedIndex = Properties.Settings.Default.OnlineModeGUID == new System.Guid(highPerformance) ? 0 : 1;

            cb_offline.Items.Add(Properties.Resources.BalancedModeTitle);
            cb_offline.Items.Add(Properties.Resources.EnergySavingTitle);
            cb_offline.SelectedIndex = Properties.Settings.Default.OfflineModeGUID == new System.Guid(balanceMode) ? 0 : 1;
        }

        private void SaveSettings()
        {
            var onlineGuid = new System.Guid(cb_online.SelectedIndex == 0 ? highPerformance : balanceMode);
            var offlineGuid = new System.Guid(cb_online.SelectedIndex == 0 ? energySaving : balanceMode);

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
