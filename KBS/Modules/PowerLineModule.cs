using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace KBS.Modules
{
    internal class PowerLineModule : Module
    {
        private PowerLineStatus _currentPowerLineStatus;

        /// <summary>
        /// Module constructor 
        ///Subscribe module on event and depending on the power status select a power plan
        /// </summary>
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
                case PowerLineStatus.Unknown:
                    NotifyUser("Unknown Power Line Status", "Power Line Status Changed", ToolTipIcon.Error);
                    break;
                default:
                    NotifyUser("Unknown Power Line Status", "Power Line Status Changed", ToolTipIcon.Error);
                    break;
            }

            _currentPowerLineStatus = SystemInformation.PowerStatus.PowerLineStatus;
        }

        /// <summary>
        /// Processing events Switching power plan and notifications to users
        /// </summary>
        private void HandleEvent(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode != PowerModes.StatusChange) return;
            switch (SystemInformation.PowerStatus.PowerLineStatus)
            {
                case PowerLineStatus.Online:
                    if (_currentPowerLineStatus != PowerLineStatus.Online)
                    {
                        DisablePowerSaving();
                        NotifyUser("Disable Power Saving", "Power Line Status Changed", ToolTipIcon.None);
                    }
                    break;
                case PowerLineStatus.Offline:
                    if (_currentPowerLineStatus != PowerLineStatus.Offline)
                    {
                        EnablePowerSaving();
                        NotifyUser("Enable Power Saving", "Power Line Status Changed", ToolTipIcon.None);
                    }
                    break;
                default:
                    if (_currentPowerLineStatus != PowerLineStatus.Unknown)
                    {
                        NotifyUser("Unknown Power Line Status", "Power Line Status Changed", ToolTipIcon.Error);
                    }
                    break;
            }

            _currentPowerLineStatus = SystemInformation.PowerStatus.PowerLineStatus;
        }

        /// <summary>
        /// Wrapper for subscription to PowerModeChanged event
        /// </summary>
        public override void EnableModule()
        {
            SystemEvents.PowerModeChanged += HandleEvent;
        }

        /// <summary>
        /// Wrapper to unsubscribe from PowerModeChanged event
        /// </summary>
        public override void DisableModule()
        {
            SystemEvents.PowerModeChanged -= HandleEvent;
        }

        /// <summary>
        /// Makes energy saving power scheme active on
        /// </summary>
        private void EnablePowerSaving() => RunCmdCommand("powercfg /s a1841308-3541-4fab-bc81-f71556f20b4a");

        /// <summary>
        /// Makes high performance power scheme active on
        /// </summary>
        private void DisablePowerSaving() => RunCmdCommand("powercfg /s 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");

        /// <summary>
        /// Wrapper for the command
        /// </summary>
        private static void RunCmdCommand(string command)
        {
            //
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
