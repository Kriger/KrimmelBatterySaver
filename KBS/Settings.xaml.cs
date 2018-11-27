using System;
using System.Collections.Generic;
using System.Windows;

namespace KBS
{
    public partial class Settings
    {
        private readonly string _highPerformance = Properties.Resources.HighPerformanceGUID;
        private readonly string _balanceMode = Properties.Resources.BalancedModeGUID;
        private readonly string _energySaving = Properties.Resources.EnergySavingGUID;

        private static readonly Dictionary<string, string> LocalizedResources = new Dictionary<string, string>();

        public Settings()
        {
            InitializeComponent();
            SetLanguageTextValues();

            Title = LocalizedResources["SettingsTitle"];
            SaveButton.Content = LocalizedResources["SaveButtonTitle"];
            CloseButton.Content = LocalizedResources["CloseButtonTitle"];
            Autostart.Content = LocalizedResources["Autostart"];
            LanguageLabel.Content = LocalizedResources["LanguageLabel"];
            PowerlineLabel.Content = LocalizedResources["PowerlineLabel"];
            BatteryLabel.Content = LocalizedResources["BatteryLabel"];

            CbLanguage.Items.Clear();
            CbLanguage.Items.Add(Properties.ResourcesRus.LanguageRusText);
            CbLanguage.Items.Add(Properties.ResourcesRus.LanguageEngText);
            CbLanguage.SelectedIndex =
                Properties.Settings.Default.SelectedLanguage == Properties.ResourcesRus.LanguageRusText ? 0 : 1;

            CbOnline.Items.Add(LocalizedResources["HighPerformanceTitle"]);
            CbOnline.Items.Add(LocalizedResources["BalancedModeTitle"]);
            CbOnline.SelectedIndex =
                Properties.Settings.Default.OnlineModeGUID == new Guid(_highPerformance) ? 0 : 1;

            CbOffline.Items.Add(LocalizedResources["BalancedModeTitle"]);
            CbOffline.Items.Add(LocalizedResources["EnergySavingTitle"]);
            CbOffline.SelectedIndex =
                Properties.Settings.Default.OfflineModeGUID == new Guid(_balanceMode) ? 0 : 1;
        }

        private void SetLanguageTextValues()
        {
            LocalizedResources.Clear();
            if (Properties.Settings.Default.SelectedLanguage == Properties.ResourcesRus.LanguageRusText)
            {
                LocalizedResources["SettingsTitle"] = Properties.ResourcesRus.SettingsTitle;
                LocalizedResources["SaveButtonTitle"] = Properties.ResourcesRus.SaveButtonTitle;
                LocalizedResources["CloseButtonTitle"] = Properties.ResourcesRus.CloseButtonTitle;
                LocalizedResources["LanguageLabel"] = Properties.ResourcesRus.LanguageLabel;
                LocalizedResources["PowerlineLabel"] = Properties.ResourcesRus.PowerlineLabel;
                LocalizedResources["BatteryLabel"] = Properties.ResourcesRus.BatteryLabel;
                LocalizedResources["Autostart"] = Properties.ResourcesRus.Autostart;

                LocalizedResources["EnergySavingTitle"] = Properties.ResourcesRus.EnergySavingTitle;
                LocalizedResources["BalancedModeTitle"] = Properties.ResourcesRus.BalancedModeTitle;
                LocalizedResources["HighPerformanceTitle"] = Properties.ResourcesRus.HighPerformanceTitle;
            }
            else
            {
                LocalizedResources["SettingsTitle"] = Properties.ResourcesEng.SettingsTitle;
                LocalizedResources["SaveButtonTitle"] = Properties.ResourcesEng.SaveButtonTitle;
                LocalizedResources["CloseButtonTitle"] = Properties.ResourcesEng.CloseButtonTitle;
                LocalizedResources["LanguageLabel"] = Properties.ResourcesEng.LanguageLabel;
                LocalizedResources["PowerlineLabel"] = Properties.ResourcesEng.PowerlineLabel;
                LocalizedResources["BatteryLabel"] = Properties.ResourcesEng.BatteryLabel;
                LocalizedResources["Autostart"] = Properties.ResourcesEng.Autostart;

                LocalizedResources["EnergySavingTitle"] = Properties.ResourcesEng.EnergySavingTitle;
                LocalizedResources["BalancedModeTitle"] = Properties.ResourcesEng.BalancedModeTitle;
                LocalizedResources["HighPerformanceTitle"] = Properties.ResourcesEng.HighPerformanceTitle;
            }
        }

        private void SaveSettings()
        {
            var onlineGuid = new Guid(CbOnline.SelectedIndex == 0 ? _highPerformance : _balanceMode);
            var offlineGuid = new Guid(CbOnline.SelectedIndex == 0 ? _energySaving : _balanceMode);

            Properties.Settings.Default["SelectedLanguage"] = CbLanguage.SelectedValue;
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