using System;
using System.Drawing;
using System.Windows.Forms;
using KNT.Modules;

namespace KNT
{
    public partial class MainWindow
    {
        private static readonly Icon Enable = Properties.Resources.enable;
        private static readonly Icon Disable = Properties.Resources.disable;

        private static readonly NotifyIcon TrayIcon = new NotifyIcon
        {
            Visible = true,
            Icon = Enable
        };

        private readonly Module _module = new PowerLineModule(TrayIcon);
        
        public MainWindow()
        {
            InitializeComponent();
            Hide();
            
            TrayIcon.ContextMenuStrip = new ContextMenuStrip();
            TrayIcon.ContextMenuStrip.Items.Add("Отключить", null, OnClickDisable);
            TrayIcon.ContextMenuStrip.Items.Add("Включить", null, OnClickEnable);
            TrayIcon.ContextMenuStrip.Items.Add("Выход", null, OnClickExit);

            TrayIcon.ContextMenuStrip.Items[1].Visible = false;
        }

        private void OnClickExit(object sender, EventArgs eventArgs)
        {
            TrayIcon.Visible = false;
            Close();
        }

        private void OnClickEnable(object sender, EventArgs eventArgs)
        {
            TrayIcon.ContextMenuStrip.Items[0].Visible = true;
            TrayIcon.ContextMenuStrip.Items[1].Visible = false;
            _module.EnableModule();
            TrayIcon.Icon = Enable;
        }

        private void OnClickDisable(object sender, EventArgs eventArgs)
        {
            TrayIcon.ContextMenuStrip.Items[0].Visible = false;
            TrayIcon.ContextMenuStrip.Items[1].Visible = true;
            _module.DisableModule();
            TrayIcon.Icon = Disable;
        }
    }
}
