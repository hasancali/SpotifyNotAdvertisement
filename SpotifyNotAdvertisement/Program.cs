using Spectre.Console;
using System.Diagnostics;
using System.Globalization;

namespace SpotifyNotAdvertisement
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// counter
        /// </summary>
        public static int counter;

        /// <summary>
        /// Main
        /// </summary>
        public static void Main(string[] args)
        {
            AnsiConsole.Write(
new FigletText("SPOTIFY").Centered()
.Color(Color.LightGreen));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Spotify AdBlocker + Now Playing");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            AnsiConsole.Markup(
"What is [lightgreen]Spotify[/] way [red](Spotify.exe)[/]: ");
            string fileWay = Console.ReadLine();

            var answerReadme = AnsiConsole.Confirm(
"Run [lightgreen]Spotify[/] ad blocker?");

            if (answerReadme)
            {
                while (true)
                {
                    Process process1;
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        process1 = Process.GetProcessesByName("Spotify").LastOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));
                        if (process1 == null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Spotify is not running");
                            Process.Start(fileWay);
                            Console.ResetColor();
                            Thread.Sleep(1000);
                        }
                        else if (process1.MainWindowTitle == "Advertisement" || process1.MainWindowTitle == "Spotify")
                        {
                            foreach (Process process2 in Process.GetProcessesByName("Spotify"))
                                process2.Kill();
                            Process.Start(fileWay);
                            Thread.Sleep(2000);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Advertisement skipped");
                            ++counter;
                            Console.ResetColor();
                            while (process1.MainWindowTitle == "Spotify")
                                Thread.Sleep(1000);
                        }
                    }
                    while (process1 == null);
                    DateTime now;
                    if (process1.MainWindowTitle != "Spotify Free")
                    {
                        Console.Title = " Spotify AdBlocker - Advertisements skipped: " + counter.ToString() + "                     " + process1.MainWindowTitle;
                        now = DateTime.Now;
                        Console.WriteLine("[" + now.ToString("HH:mm:ss", (IFormatProvider)DateTimeFormatInfo.InvariantInfo) + "]  " + process1.MainWindowTitle);
                    }
                    else
                    {
                        now = DateTime.Now;
                        Console.WriteLine("[" + now.ToString("HH:mm:ss", (IFormatProvider)DateTimeFormatInfo.InvariantInfo) + "]  Spotify (Paused)");
                        Console.Title = " Spotify AdBlocker - Advertisements skipped: " + counter.ToString() + "                     Spotify (Paused)";
                    }
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
