using System.Windows.Forms;

namespace KBS.Modules
{
    internal abstract class Module
    {
        private readonly NotifyIcon _notifyIcon;

        protected Module(NotifyIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;
        }
        
        public abstract void EnableModule();
        public abstract void DisableModule();


        protected void NotifyUser(string title, string text, ToolTipIcon icon)
        {
            _notifyIcon.BalloonTipIcon = icon;
            _notifyIcon.BalloonTipTitle = title;
            _notifyIcon.BalloonTipText = text;
            
            _notifyIcon.ShowBalloonTip(2000);
        }
    }
}
