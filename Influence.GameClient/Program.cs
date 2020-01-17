using Influence.Services.Bot;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Influence.Common.Extensions;

namespace Influence.GameClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
                RunFromConsole(args);
            else
                HideConsoleAndRunFromGui();
        }

        private static void HideConsoleAndRunFromGui()
        {
            string TitleGuid = Guid.NewGuid().ToString();
            Console.Title = TitleGuid;
            IntPtr hWnd = FindWindow(null, "Your console windows caption"); //put your console window caption here
            if (hWnd != IntPtr.Zero)
                ShowWindow(hWnd, 0); // 0 = SW_HIDE
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameClient());
        }

        private static void RunFromConsole(string[] args)
        {
            string botname = null;
            string sessionId = null;
            string serverUrl = null;
            Guid botGuid = Guid.Empty;
            for (int i = 0; i < args.Length; ++i)
            {
                if (args.Length - 1 == i)
                    break;
                else if (args[i].Equals("-name", StringComparison.CurrentCultureIgnoreCase))
                    botname = args[++i];
                else if (args[i].Equals("-session", StringComparison.CurrentCultureIgnoreCase))
                    sessionId = args[++i];
                else if (args[i].Equals("-serverurl", StringComparison.CurrentCultureIgnoreCase))
                    serverUrl = args[++i];
                else if (args[i].Equals("-botguid", StringComparison.CurrentCultureIgnoreCase))
                    botGuid = new Guid(args[++i]);
            }
            if (botname is null || sessionId is null || serverUrl is null || botGuid.NotValid())
                PrintHelp();
            else
            {
                var master = new BotMaster(botname, serverUrl, sessionId, botGuid);
                master.Run();
            }
        }

        private static void PrintHelp()
            => Console.WriteLine("Usage: Influence.GameClient.exe -name botname -serverurl http://serverdomain/ws.ashx -session sessionguid");

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
