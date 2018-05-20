using System.Windows;

namespace KBS
{
    public partial class Settings
    {
        private string highPerformance = KBS.Properties.Resources.HighPerformanceGUID;
        private string balanceMode = KBS.Properties.Resources.BalancedModeGUID;
        private string energySaving = KBS.Properties.Resources.EnergySavingGUID;

        public Settings()
        {
            InitializeComponent();

            cb_language.Items.Clear();
            cb_language.Items.Add("Русский");
            cb_language.Items.Add("English");
            cb_language.SelectedIndex = 0;

            cb_online.Items.Add(KBS.Properties.Resources.HighPerformanceTitle);
            cb_online.Items.Add(KBS.Properties.Resources.BalancedModeTitle);
            cb_online.SelectedIndex = KBS.Properties.Settings.Default.OnlineModeGUID == new System.Guid(highPerformance) ? 0 : 1;

            cb_offline.Items.Add(KBS.Properties.Resources.BalancedModeTitle);
            cb_offline.Items.Add(KBS.Properties.Resources.EnergySavingTitle);
            cb_offline.SelectedIndex = KBS.Properties.Settings.Default.OfflineModeGUID == new System.Guid(balanceMode) ? 0 : 1;
        }

        private void SaveSettings()
        {
            var onlineGuid = new System.Guid(cb_online.SelectedIndex == 0 ? highPerformance : balanceMode);
            var offlineGuid = new System.Guid(cb_online.SelectedIndex == 0 ? energySaving : balanceMode);

            KBS.Properties.Settings.Default["SelectedLanguage"] = cb_language.SelectedValue;
            KBS.Properties.Settings.Default["OnlineModeGUID"] = onlineGuid;
            KBS.Properties.Settings.Default["OfflineModeGUID"] = offlineGuid;
            KBS.Properties.Settings.Default.Save();
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
