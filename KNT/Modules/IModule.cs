using System.Windows.Forms;
using Microsoft.Win32;

namespace KNT.Modules
{
    internal abstract class Module
    {
        private readonly NotifyIcon _notifyIcon;

        protected Module(NotifyIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;
        }

        //public abstract void HandleEvent(object sender, PowerModeChangedEventArgs e);
        public abstract void EnableModule();
        public abstract void DisableModule();


        protected void NotifyUser(string title, string text, ToolTipIcon icon)
        {
            _notifyIcon.BalloonTipIcon = icon;
            _notifyIcon.BalloonTipTitle = title;
            _notifyIcon.BalloonTipText = text;

            //_notifyIcon.Visible = true;
            _notifyIcon.ShowBalloonTip(1000);
            //_notifyIcon.Visible = false;
        }
    }
}
