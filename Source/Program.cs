using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WatiN.Core;
using System.Windows.Forms;

namespace K9AutoEnter
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Please ensure all K9 windows are closed.");
            Console.WriteLine("");
            Console.WriteLine("Please enter the K9 password:");
            string k9password = Console.ReadLine();

            
            using (var browser = new IE("http://127.0.0.1:2372/overrides"))
            {
                try
                {
                    browser.BringToFront();

                    browser.TextField(Find.ById("b-loginContain")).TypeText(k9password);
                    browser.Element(Find.ById("o-loginContain")).Click();

                    string path_here = AppDomain.CurrentDomain.BaseDirectory;
                    string txt_name = "K9Exceptions.txt";
                    string[] lines = System.IO.File.ReadAllLines(path_here + txt_name);
                    foreach (string line in lines)
                    {
                        browser.TextField(Find.ById("listTb-1")).TypeText(line);
                        System.Threading.Thread.Sleep(10);
                        System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                        browser.Element(Find.ById("listAdd-1")).Click();
                    }

                    browser.Element(Find.ById("y-saveChanges-0")).Click();

                    Console.WriteLine("Exceptions have been entered.");
                    Console.WriteLine("Press the enter key to exit the application.");
                    Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine("There was an error entering the exceptions.");
                }

            }
        }
    }
}
