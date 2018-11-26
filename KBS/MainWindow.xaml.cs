using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using KBS.Modules;

namespace KBS
{
    public partial class MainWindow
    {
        private static readonly Icon Enable = Properties.Resources.Enable;
        private static readonly Icon Disable = Properties.Resources.Disable;
        private static readonly Dictionary<string, string> LocalizedResources = new Dictionary<string, string>();

        private static readonly NotifyIcon TrayIcon = new NotifyIcon
        {
            Visible = true,
            Icon = Enable,
            Text = Properties.ResourcesRus.TipEnableText
        };

        private readonly Module _powerLineModule = new PowerLineModule(TrayIcon);
        private readonly Module _changePowerModeModule = new ChangePowerModeModule(TrayIcon);

        public MainWindow()
        {
            InitializeComponent();
            Hide();
            InitializeTrayIcon();
        }

        private ToolStripDropDownButton BuildToolStripDropDownButton()
        {
            var subMenu = new ToolStripDropDownButton(LocalizedResources["SubMenuTitle"]);
            subMenu.DropDownItems.Add(LocalizedResources["EnergySavingTitle"], null, OnClickEnergySaving);
            subMenu.DropDownItems.Add(LocalizedResources["BalancedModeTitle"], null, OnClickBalancedMode);
            subMenu.DropDownItems.Add(LocalizedResources["HighPerformanceTitle"], null, OnClickHighPerformance);

            int currentSchemeIndex = -1;
            switch (GetCurrentSchemeGUID())
            {
                case "a1841308-3541-4fab-bc81-f71556f20b4a":
                    currentSchemeIndex = 0;
                    break;
                case "381b4222-f694-41f0-9685-ff5bb260df2e":
                    currentSchemeIndex = 1;
                    break;
                case "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c":
                    currentSchemeIndex = 2;
                    break;
            }

            subMenu.DropDownItems[currentSchemeIndex].Font = new Font(subMenu.DropDownItems[currentSchemeIndex].Font,
                System.Drawing.FontStyle.Bold);

            return subMenu;
        }

        private string GetCurrentSchemeGUID()
        {
            var cmd = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };
            cmd.Start();

            var reader = cmd.StandardOutput;

            cmd.StandardInput.WriteLine(Properties.Resources.GetCurrentShemeCmd);
            var input = string.Empty;
            while (input?.Contains("GUID") == false)
            {
                input = reader.ReadLine();
            }

            var output = string.Empty;
            if (input == null)
                return output;

            output = input.Split(':')[1].Split(' ')[1];
            Console.WriteLine(output);
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            return output;
        }

        private void InitializeTrayIcon()
        {
            SetLanguageTextValues();
            
            TrayIcon.ContextMenuStrip = new ContextMenuStrip();
            TrayIcon.ContextMenuStrip.Items.Add(LocalizedResources["DisableText"], null, OnClickDisable);
            TrayIcon.ContextMenuStrip.Items.Add(LocalizedResources["EnableText"], null, OnClickEnable);
            TrayIcon.ContextMenuStrip.Items.Add(BuildToolStripDropDownButton());
            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            TrayIcon.ContextMenuStrip.Items.Add(LocalizedResources["SettingsText"], null, OnClickSettings);
            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            TrayIcon.ContextMenuStrip.Items.Add(LocalizedResources["ExitText"], null, OnClickExit);

            TrayIcon.ContextMenuStrip.Items[1].Visible = false;
        }

        private void SetLanguageTextValues()
        {
            LocalizedResources.Clear();
            if (Properties.Settings.Default.SelectedLanguage == Properties.ResourcesRus.LanguageRusText)
            {
                LocalizedResources["DisableText"] = Properties.ResourcesRus.DisableText;
                LocalizedResources["EnableText"] = Properties.ResourcesRus.EnableText;
                LocalizedResources["SettingsText"] = Properties.ResourcesRus.SettingsText;
                LocalizedResources["ExitText"] = Properties.ResourcesRus.ExitText;
                LocalizedResources["SubMenuTitle"] = Properties.ResourcesRus.SubMenuTitle;
                LocalizedResources["EnergySavingTitle"] = Properties.ResourcesRus.EnergySavingTitle;
                LocalizedResources["BalancedModeTitle"] = Properties.ResourcesRus.BalancedModeTitle;
                LocalizedResources["HighPerformanceTitle"] = Properties.ResourcesRus.HighPerformanceTitle;
            }
            else
            {
                LocalizedResources["DisableText"] = Properties.ResourcesEng.DisableText;
                LocalizedResources["EnableText"] = Properties.ResourcesEng.EnableText;
                LocalizedResources["SettingsText"] = Properties.ResourcesEng.SettingsText;
                LocalizedResources["ExitText"] = Properties.ResourcesEng.ExitText;
                LocalizedResources["SubMenuTitle"] = Properties.ResourcesEng.SubMenuTitle;
                LocalizedResources["EnergySavingTitle"] = Properties.ResourcesEng.EnergySavingTitle;
                LocalizedResources["BalancedModeTitle"] = Properties.ResourcesEng.BalancedModeTitle;
                LocalizedResources["HighPerformanceTitle"] = Properties.ResourcesEng.HighPerformanceTitle;
            }
        }

        private void OnClickExit(object sender, EventArgs eventArgs)
        {
            TrayIcon.Dispose();
            Close();
        }

        private void OnClickEnergySaving(object sender, EventArgs eventArgs)
        {
            ((ChangePowerModeModule) _changePowerModeModule).SetPowerSavingMode();
            InitializeTrayIcon();
        }

        private void OnClickBalancedMode(object sender, EventArgs eventArgs)
        {
            ((ChangePowerModeModule) _changePowerModeModule).SetBalanceMode();
            InitializeTrayIcon();
        }

        private void OnClickHighPerformance(object sender, EventArgs eventArgs)
        {
            ((ChangePowerModeModule) _changePowerModeModule).SetHighPerformanceMode();
            InitializeTrayIcon();
        }

        private void OnClickSettings(object sender, EventArgs eventArgs)
        {
            var settings = new Settings();

            settings.ShowDialog();
            
            InitializeTrayIcon();
        }

        private void OnClickEnable(object sender, EventArgs eventArgs)
        {
            TrayIcon.ContextMenuStrip.Items[0].Visible = true;
            TrayIcon.ContextMenuStrip.Items[1].Visible = false;
            _powerLineModule.EnableModule();
            TrayIcon.Icon = Enable;
            TrayIcon.Text = Properties.ResourcesRus.TipEnableText;
        }

        private void OnClickDisable(object sender, EventArgs eventArgs)
        {
            TrayIcon.ContextMenuStrip.Items[0].Visible = false;
            TrayIcon.ContextMenuStrip.Items[1].Visible = true;
            _powerLineModule.DisableModule();
            TrayIcon.Icon = Disable;
            TrayIcon.Text = Properties.ResourcesRus.TipDisableText;
        }
    }
}
