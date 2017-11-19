﻿using System;
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

        public MainWindow()
        {
            InitializeComponent();
            Hide();
            InitializeTrayIcon();
        }

        private void InitializeTrayIcon()
        {
            var subMenu = new ToolStripDropDownButton("Выбрать план");
            subMenu.DropDownItems.Add("Экономия заряда", null, OnClickEnergySaving);
            subMenu.DropDownItems.Add("Сбалансированный режим", null, OnClickBalancedMode);
            subMenu.DropDownItems.Add("Высокая производительность", null, OnClickHighPerformance);

            TrayIcon.ContextMenuStrip = new ContextMenuStrip();
            TrayIcon.ContextMenuStrip.Items.Add(Properties.Resources.DisableText, null, OnClickDisable);
            TrayIcon.ContextMenuStrip.Items.Add(Properties.Resources.EnableText, null, OnClickEnable);
            TrayIcon.ContextMenuStrip.Items.Add(subMenu);
            TrayIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            TrayIcon.ContextMenuStrip.Items.Add("Настройки", null, OnClickSettings);
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

        }

        private void OnClickBalancedMode(object sender, EventArgs eventArgs)
        {

        }

        private void OnClickHighPerformance(object sender, EventArgs eventArgs)
        {

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
