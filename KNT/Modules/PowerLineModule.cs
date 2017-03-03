using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace KNT.Modules
{
    class PowerLineModule : Module
    {
        public PowerLineModule(NotifyIcon notifyIcon) : base(notifyIcon)
        {
            SystemEvents.PowerModeChanged += HandleEvent;
            
            switch (SystemInformation.PowerStatus.PowerLineStatus)
            {
                case PowerLineStatus.Online:
                    DisablePowerSaving();
                    break;
                case PowerLineStatus.Offline:
                    EnablePowerSaving();
                    break;
                default:
                    NotifyUser("Unknown Power Line Status", "Power Line Status Changed", ToolTipIcon.Error);
                    break;
            }
        }

        public override void HandleEvent(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode != PowerModes.StatusChange) return;
            switch (SystemInformation.PowerStatus.PowerLineStatus)
            {
                case PowerLineStatus.Online:
                    DisablePowerSaving();
                    NotifyUser("Disable Power Saving", "Power Line Status Changed", ToolTipIcon.None);
                    break;
                case PowerLineStatus.Offline:
                    EnablePowerSaving();
                    NotifyUser("Enable Power Saving", "Power Line Status Changed", ToolTipIcon.None);
                    break;
                default:
                    NotifyUser("Unknown Power Line Status", "Power Line Status Changed", ToolTipIcon.Error);
                    break;
            }
        }
        
        public override void EnableModule()
        {
            SystemEvents.PowerModeChanged += HandleEvent;
        }

        public override void DisableModule()
        {
            SystemEvents.PowerModeChanged -= HandleEvent;
        }

        private void EnablePowerSaving()
        {
            RunCmdCommand("powercfg /s a1841308-3541-4fab-bc81-f71556f20b4a");
        }

        private void DisablePowerSaving()
        {
            RunCmdCommand("powercfg /s 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");
        }

        private static void RunCmdCommand(string command)
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

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }
    }
}
