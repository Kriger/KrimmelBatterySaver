using System;
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

            return subMenu;
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
        }

        private void OnClickBalancedMode(object sender, EventArgs eventArgs)
        {
            ((ChangePowerModeModule)_changePowerModeModule).SetBalanceMode();
        }

        private void OnClickHighPerformance(object sender, EventArgs eventArgs)
        {
            ((ChangePowerModeModule)_changePowerModeModule).SetHighPerformanceMode();
        }

        private void OnClickSettings(object sender, EventArgs eventArgs)
        {
            
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
