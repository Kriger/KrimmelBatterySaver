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
        private readonly Module _module = new PowerLineModule(TrayIcon);

        public MainWindow()
        {
            InitializeComponent();
            Hide();

            TrayIcon.ContextMenuStrip = new ContextMenuStrip();
            TrayIcon.ContextMenuStrip.Items.Add(Properties.Resources.DisableText, null, OnClickDisable);
            TrayIcon.ContextMenuStrip.Items.Add(Properties.Resources.EnableText, null, OnClickEnable);
            TrayIcon.ContextMenuStrip.Items.Add(Properties.Resources.ExitText, null, OnClickExit);

            TrayIcon.ContextMenuStrip.Items[1].Visible = false;
        }

        private void OnClickExit(object sender, EventArgs eventArgs)
        {
            TrayIcon.Dispose();
            Close();
        }

        private void OnClickEnable(object sender, EventArgs eventArgs)
        {
            TrayIcon.ContextMenuStrip.Items[0].Visible = true;
            TrayIcon.ContextMenuStrip.Items[1].Visible = false;
            _module.EnableModule();
            TrayIcon.Icon = Enable;
            TrayIcon.Text = Properties.Resources.TipEnableText;
        }

        private void OnClickDisable(object sender, EventArgs eventArgs)
        {
            TrayIcon.ContextMenuStrip.Items[0].Visible = false;
            TrayIcon.ContextMenuStrip.Items[1].Visible = true;
            _module.DisableModule();
            TrayIcon.Icon = Disable;
            TrayIcon.Text = Properties.Resources.TipDisableText;
        }
    }
}
