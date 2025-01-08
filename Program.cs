using System.Diagnostics;
using System.Runtime.InteropServices;

// me when i know im about to get my stuff skidded

class Program
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool OpenClipboard(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool EmptyClipboard();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool CloseClipboard();

    private const uint CF_UNICODETEXT = 13;

    //pc                                                                                 b y t e

    static async Task Main(string[] args)
    {
            await startingsomethingthingy();
    }

    static async Task startingsomethingthingy()
    {
        Console.Clear();

        while (true)
        {
            Console.Write("Enter text: ");
            string userInput = Console.ReadLine();

            string transformedText = beforeaftertransform(userInput);
            Console.WriteLine(transformedText);

            await copyclipboard(transformedText);

            Console.WriteLine("Text copied.");
            Console.WriteLine("Press [Enter] to continue or type 'exit' to quit.");

            string userChoice = Console.ReadLine();
            if (userChoice.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting Chat Bypasser...");
                Environment.Exit(0);
            }
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(exePath);
            Environment.Exit(0);
        }
    }



    static string beforeaftertransform(string input)
    {
        string transformedText = "";
        foreach (char c in input)
        {
            if (c == ' ')
                transformedText += "...";
            else if (c == 'u' || c == 'U')
                transformedText += "Ü.";
            else if (c == 'g' || c == 'G')
                transformedText += "Ǥ.";
            else if (c == 'i' || c == 'I')
                transformedText += "í.";
            else if (c == 'y' || c == 'Y')
                transformedText += "Ỷ.";
            else
                transformedText += c + ".";
        }

        if (transformedText.EndsWith("."))
            transformedText = transformedText.Substring(0, transformedText.Length - 1);

        return transformedText;
    }

    static async Task copyclipboard(string text)
    {
        try
        {
            if (!OpenClipboard(IntPtr.Zero))
            {
                Console.WriteLine("i forgot how to open the clipping boardy thingy 🤔");
                return;
            }

            if (!EmptyClipboard())
            {
                Console.WriteLine("i cant empty clipboarding 😭😭😭");
                return;
            }

            IntPtr hGlobal = Marshal.AllocHGlobal((text.Length + 1) * sizeof(char)); 
            try
            {
                Marshal.Copy(text.ToCharArray(), 0, hGlobal, text.Length);
                Marshal.WriteInt16(hGlobal + text.Length * sizeof(char), 0); 

                if (SetClipboardData(CF_UNICODETEXT, hGlobal) == IntPtr.Zero)
                {
                    Console.WriteLine("i cant set clipboard!!! 😡😡🤬🤬🤬");
                }
                else
                {
                    Console.WriteLine("😊😊😊 i set clipboard!!!!");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(hGlobal);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        }
        finally
        {
            CloseClipboard();
        }
        await Task.Delay(100);
    }

}
