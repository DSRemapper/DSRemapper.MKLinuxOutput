using System;
using System.ComponentModel;
using System.Diagnostics;

namespace DSRemapper.MKLinuxOutput
{
    internal enum DoToolType{
        None,
        Xdotool,
        Ydotool,
    }
    internal class DoTool
    {
        private static Dictionary<DoToolType,string> tools = new()
        {
            {DoToolType.Xdotool, "xdotool"},
            {DoToolType.Ydotool, "ydotool"}
        };
        private readonly DoToolType tool;
        private readonly ProcessStartInfo startInfo;
        private readonly Queue<string> pendingActions = new();
        public DoTool(DoToolType tool)
        {
            this.tool = tool;
            startInfo = new(tools[tool]);
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
        }
        public DoTool KeyUp(int keyCode)
        {
            
            return this;
        }
        public DoTool KeyDown(int keyCode)
        {
            
            return this;
        }
        public DoTool KeyPress(int keyCode)
        {

            return this;
        }

        public void Execute()
        {
            while(pendingActions.TryDequeue(out var action))
                startInfo.ArgumentList.Add(action);
        }
        internal static bool CommandExists(string commandName)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "/bin/bash";
                    process.StartInfo.Arguments = $"-c \"command -v {commandName}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}