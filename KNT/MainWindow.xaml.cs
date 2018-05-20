using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using KNT.Modules;

namespace KNT
{
    public partial class MainWindow
    {
        private static readonly Icon Enable = Properties.Resources.Enable;
        private static readonly Icon Disable = Properties.Resources.Disable;

        private static readonly NotifyIcon TrayIcon = new NotifyIcon { Visible = true, Icon = Enable, Text = Properties.Resources.TipEnableText };
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
            var subMenu = new ToolStripDropDownButton(Properties.Resources.SubMenuTitle);
            subMenu.DropDownItems.Add(Properties.Resources.EnergySavingTitle, null, OnClickEnergySaving);
            subMenu.DropDownItems.Add(Properties.Resources.BalancedModeTitle, null, OnClickBalancedMode);
            subMenu.DropDownItems.Add(Properties.Resources.HighPerformanceTitle, null, OnClickHighPerformance);


            int currentShemeIndex = -1;
            switch(GetCurrentSchemeGUID())
            {
                case "a1841308-3541-4fab-bc81-f71556f20b4a":
                    currentShemeIndex = 0;
                    break;
                case "381b4222-f694-41f0-9685-ff5bb260df2e":
                    currentShemeIndex = 1;
                    break;
                case "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c":
                    currentShemeIndex = 2;
                    break;
            }

            subMenu.DropDownItems[currentShemeIndex].Font = new Font(subMenu.DropDownItems[currentShemeIndex].Font, System.Drawing.FontStyle.Bold);   

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
            var output = string.Empty;
            while (output != null && !output.Contains("GUID"))
            {
                output = reader.ReadLine();
            }
            Console.WriteLine(output?.Split(':')[1].Split(' ')[1]);
            cmd.StandardInput.Close();
            cmd.WaitForExit();

            return output?.Split(':')[1].Split(' ')[1];
        }

        private void InitializeTrayIcon()
        {
            TrayIcon.ContextMenuStrip = new ContextMenuStrip();
            TrayIcon.ContextMenuStrip.Items.Add(Properties.Resources.DisableText, null, OnClickDisable);
            TrayIcon.ContextMenuStrip.Items.Add(Properties.Resources.EnableText, null, OnClickEnable);
            TrayIcon.ContextMenuStrip.Items.Add(BuildToolStripDropDownButton());
            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            TrayIcon.ContextMenuStrip.Items.Add(Properties.Resources.SettingsText, null, OnClickSettings);
            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            TrayIcon.ContextMenuStrip.Items.Add(Properties.Resources.ExitText, null, OnClickExit);

            TrayIcon.ContextMenuStrip.Items[1].Visible = false;
        }

        private void OnClickExit(object sender, EventArgs eventArgs)
        {
            TrayIcon.Dispose();
            Close();
        }

        private void OnClickEnergySaving(object sender, EventArgs eventArgs)
        {
            ((ChangePowerModeModule)_changePowerModeModule).SetPowerSavingMode();
            InitializeTrayIcon();
        }

        private void OnClickBalancedMode(object sender, EventArgs eventArgs)
        {
            ((ChangePowerModeModule)_changePowerModeModule).SetBalanceMode();
            InitializeTrayIcon();
        }

        private void OnClickHighPerformance(object sender, EventArgs eventArgs)
        {
            ((ChangePowerModeModule)_changePowerModeModule).SetHighPerformanceMode();
            InitializeTrayIcon();
        }

        private void OnClickSettings(object sender, EventArgs eventArgs)
        {
            var settings = new Settings();

            settings.ShowDialog();
        }

        private void OnClickEnable(object sender, EventArgs eventArgs)
        {
            TrayIcon.ContextMenuStrip.Items[0].Visible = true;
            TrayIcon.ContextMenuStrip.Items[1].Visible = false;
            _powerLineModule.EnableModule();
            TrayIcon.Icon = Enable;
            TrayIcon.Text = Properties.Resources.TipEnableText;
        }

        private void OnClickDisable(object sender, EventArgs eventArgs)
        {
            TrayIcon.ContextMenuStrip.Items[0].Visible = false;
            TrayIcon.ContextMenuStrip.Items[1].Visible = true;
            _powerLineModule.DisableModule();
            TrayIcon.Icon = Disable;
            TrayIcon.Text = Properties.Resources.TipDisableText;
        }
    }
}
