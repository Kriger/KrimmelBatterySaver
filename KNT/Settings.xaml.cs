using System.Windows;

namespace KNT
{
    public partial class Settings
    {
        public Settings()
        {
            InitializeComponent();

            cb_language.Items.Clear();
            cb_language.Items.Add("Русский");
            cb_language.Items.Add("English");
            cb_language.SelectedIndex = 0;

            cb_online.Items.Add("Высокая производительность");
            cb_online.Items.Add("Сбалансированный режим");
            cb_online.SelectedIndex = 0;

            cb_offline.Items.Add("Экономия заряда");
            cb_offline.Items.Add("Сбалансированный режим");
            cb_offline.SelectedIndex = 0;
        }

        private void SaveSettings()
        {
            
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
