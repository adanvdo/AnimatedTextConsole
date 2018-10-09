using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AnimatedTextConsole
{
    class Program
    {
        static string testfilepath = string.Empty;
        static int chardelay = 30;
        static int linedelay = 800;
        static bool doublespace = false;
        static bool randomizelinedelay = false;
        static int defscreenwidth = Convert.ToInt32(Math.Round(Console.LargestWindowWidth * .25, 0, MidpointRounding.ToEven));
        static int defscreenheight = Convert.ToInt32(Math.Round(Console.LargestWindowHeight * .40, 0, MidpointRounding.ToEven));
        static int screenwidth = defscreenwidth;
        static int screenheight = defscreenheight;
        static List<string> textlines = new List<string>();

        static void Main(string[] args)
        {
            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-w")
                    {
                        bool valid = Int32.TryParse(args[i + 1], out screenwidth);
                        
                        if (valid && screenwidth > Console.LargestWindowWidth)
                            screenwidth = defscreenwidth;
                        if (valid && screenheight > Console.LargestWindowHeight)
                            screenheight = defscreenheight;

                    }
                    if (args[i] == "-h")
                    {
                        bool valid = Int32.TryParse(args[i + 1], out screenheight);
                    }
                    if (args[i] == "-cd")
                    {
                        bool valid = Int32.TryParse(args[i + 1], out chardelay);
                        if (!valid)
                        {
                            Console.WriteLine("Invalid Delay Argument");
                            showHelp();
                            return;
                        }
                    }
                    if (args[i] == "-ld")
                    {
                        bool valid = Int32.TryParse(args[i + 1], out linedelay);
                        if (!valid)
                        {
                            Console.WriteLine("Invalid Delay Argument");
                            showHelp();
                            return;
                        }
                    }
                    if (args[i] == "-ds")
                    {
                        doublespace = true;
                    }
                    if (args[i] == "-rld")
                    {
                        randomizelinedelay = true;
                    }
                    if (args[i] == "-txt")
                    {
                        string allargs = string.Join(" ", args);
                        //Console.WriteLine("all args: " + allargs);
                        int i1 = allargs.IndexOf("-txt") + 4;
                        //Console.WriteLine("txt index: " + i1.ToString());
                        string aftertxt = allargs.Substring(i1, allargs.Length - i1).TrimStart(' ');
                        //Console.WriteLine("after txt: " + aftertxt);
                        if (aftertxt.IndexOf("\"") == null || aftertxt.IndexOf("\"") < 0)
                        {
                            Console.WriteLine("Invalid Filepath Format");
                            showHelp();
                            return;
                        }
                        string filepath = aftertxt.Substring(aftertxt.IndexOf("\"") + 1, aftertxt.LastIndexOf("\"") - 1);
                        //Console.WriteLine("filepath: " + filepath);
                        if (!File.Exists(filepath))
                        {
                            Console.WriteLine("Invalid Text File Path");
                            showHelp();
                            return;
                        }
                        string line;
                        StreamReader file = new StreamReader(filepath);
                        while ((line = file.ReadLine()) != null)
                        {
                            textlines.Add(line);
                        }
                        file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WindowWidth = screenwidth;
            Console.WindowHeight = screenheight;
            Console.WriteLine("Press any key to begin animation");
            Console.ReadKey();

            Console.Clear();

            for (int li = 0; li < textlines.Count; li++)
            {
                string line = "   " + textlines[li];
                int chars = line.Length;
                if (!doublespace)
                    Console.WriteLine();
                else Console.WriteLine(Environment.NewLine);
                for (int i = 1; i < chars + 1; i++)
                {
                    Console.Write("\r{0}", line.Substring(0, i));
                    if(i > 2) Thread.Sleep(chardelay);
                }
                if (randomizelinedelay)
                {
                    Random r = new Random();
                    int ld = r.Next(0, 500);
                    int neg = r.Next(0, 2);
                    if (neg == 0)
                        ld = 0 - ld;
                     if(li < textlines.Count - 1) Thread.Sleep(linedelay + ld < 0 ? 0 : linedelay + ld);
                }
                else if(li < textlines.Count - 1) Thread.Sleep(linedelay);
            }
            Console.WriteLine(Environment.NewLine);
            Console.Write("   ");

            Console.ReadKey();
        }

        static void showHelp()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("AnimatedTextConsole Arguments");
            Console.WriteLine();
            Console.WriteLine(" -cd  | set delay in ms between characters         | example: -cd 30   ");
            Console.WriteLine(" -ld  | set delay in ms before next line           | example: -ld 800  ");
            Console.WriteLine(" -rld | randomize line delay between by +/- 500 ms | example: -rld ");
            Console.WriteLine(" -ds  | enable double spacing between lines        | example: -ds ");
            Console.WriteLine(" -txt | set txt filepath with lines to display     | example: -txt \"" + @"c:\users\bobdole\downloads\textlines.txt" + "\" ");
            Console.WriteLine(" -w   | set window width in columns                | example: -w 50 ");
            Console.WriteLine(" -h   | set window height in columns               | example: -h 50 ");
            Console.ReadKey();
        }
    }
}
