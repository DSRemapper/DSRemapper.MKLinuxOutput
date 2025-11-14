using DSRemapper.Core;
using DSRemapper.Types;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace DSRemapper.MKLinuxOutput
{
    /// <summary>
    /// Keyboard controller class
    /// </summary>
    [EmulatedController("Linux/Keyboard")]
    public class Keyboard : IDSROutputController
    {
        private static readonly DSRLogger logger = DSRLogger.GetLogger("DSRemapper.MKLinuxOutput/Keyboard");

        private static readonly DoToolType tool;

        /// <inheritdoc/>
        public bool IsConnected { get; private set; }
        /// <inheritdoc/>
        public IDSRInputReport State { get => null!; set => _ = value; }
        static Keyboard()
        {

            if (DoTool.CommandExists("ydotool"))
                tool = DoToolType.Ydotool;
            else if (DoTool.CommandExists("xdotool"))
            {
                tool = DoToolType.Xdotool;
                logger.LogWarning("Fall back from 'ydotool' to 'xdotool'. If you can install 'ydotool'.");
            }
            else{
                tool = DoToolType.None;
                logger.LogError("Neither 'ydotool' or 'xdotool' is installed in your system. Please install one of them.");
            }
        }
        /// <summary>
        /// Keyboard controller class constructor
        /// </summary>
        public Keyboard()
        {
            
        }
        /// <inheritdoc/>
        public void Connect()
        {
            IsConnected = tool != DoToolType.None;
        }
        /// <inheritdoc/>
        public void Disconnect()
        {
            IsConnected = false;
        }
        /// <inheritdoc/>
        public void Dispose()
        {
            Disconnect();
            GC.SuppressFinalize(this);
        }
        /// <inheritdoc/>
        public IDSROutputReport GetFeedbackReport() => new DefaultDSROutputReport();
        /// <inheritdoc/>
        public void Update()
        {
            if (IsConnected)
            {
                
            }
        }

    }
}