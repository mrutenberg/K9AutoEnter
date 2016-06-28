using System;
using System.IO;
using WatiN.Core;
using WatiN.Core.Native.Windows;

namespace K9AutoEnter
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {

            Console.WriteLine("K9AutoEnter");
            Console.WriteLine("");
            Console.WriteLine("Please enter the K9 password:");
            string k9password = ReadPassword();

            using (WebBrowser._browser)
            {
                try
                {
                    WebBrowser._browser.AutoClose = false;

                    //login to K9
                    WebBrowser._browser.TextField(Find.ById("b-loginContain")).TypeText(k9password);
                    WebBrowser._browser.Element(Find.ById("o-loginContain")).Click();

                    WebBrowser._browser.BringToFront();

                    //K9AllowList
                    AddExceptions("Allow", "listTb-1", "listAdd-1");

                    //K9BlockList
                    AddExceptions("Block", "listTb-0", "listAdd-0");
                }
                catch
                {
                    Console.WriteLine("Somthing went wrong!");
                }
            }

            //close up
            Console.WriteLine("Press the enter key to exit the application.");
            Console.ReadLine();
        }


        public static class WebBrowser
        {
            public static IE _browser;

            static WebBrowser()
            {
                _browser = new IE("http://127.0.0.1:2372/overrides");
            }

            public static IE Current
            {
                get { return _browser; }
                set { _browser = value; }
            }
        }

        public static void AddExceptions(string exceptions_type, string exceptions_txtfld, string exceptions_addbtn)
            {
                using (WebBrowser._browser)
                {
                    try
                    {
                        //WebBrowser._browser.GoTo("http://127.0.0.1:2372/overrides");

                        string local_dir_path = AppDomain.CurrentDomain.BaseDirectory;
                        string full_file_name = local_dir_path + "K9" + exceptions_type + "List.txt";
                        if (!File.Exists(full_file_name)) { return; }
                        string[] lines = System.IO.File.ReadAllLines(full_file_name);
                        foreach (string line in lines)
                        {
                            WebBrowser._browser.TextField(Find.ById(exceptions_txtfld)).TypeText(line);
                            System.Threading.Thread.Sleep(5);
                            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                            WebBrowser._browser.Element(Find.ById(exceptions_addbtn)).Click();
                            System.Threading.Thread.Sleep(5);
                            WebBrowser._browser.Element(Find.ById(exceptions_addbtn)).Click();

                        }
                        WebBrowser._browser.Element(Find.ById("y-saveChanges-0")).Click();
                        Console.WriteLine(exceptions_type + " exceptions have been entered.");
                    }
                    catch
                    {
                        Console.WriteLine("There was an error entering the " + exceptions_type + " exceptions.");
                    }
            }
        }

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }
    }
}
