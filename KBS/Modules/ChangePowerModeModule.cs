using System.Diagnostics;
using System.Windows.Forms;

namespace KBS.Modules
{
    internal class ChangePowerModeModule : Module
    {
        public ChangePowerModeModule(NotifyIcon notifyIcon) : base(notifyIcon)
        {
            
        }

        public override void DisableModule()
        {

        }

        public override void EnableModule()
        {

        }

        /// <summary>
        /// Makes energy saving power scheme active on
        /// </summary>
        public void SetPowerSavingMode() => RunCmdCommand("powercfg /s a1841308-3541-4fab-bc81-f71556f20b4a");

        /// <summary>
        /// Makes energy saving power scheme active on
        /// </summary>
        public void SetBalanceMode() => RunCmdCommand("powercfg /s 381b4222-f694-41f0-9685-ff5bb260df2e");

        /// <summary>
        /// Makes high performance power scheme active on
        /// </summary>
        public void SetHighPerformanceMode() => RunCmdCommand("powercfg /s 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");

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
