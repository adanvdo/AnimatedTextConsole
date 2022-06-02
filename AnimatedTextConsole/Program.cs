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
        static int dotdelay = 300;
        static int linedelay = 800;
        static int emptyLineDelay = 10;
        static bool doublespace = false;
        static bool randomizelinedelay = false;
        static bool clearbeforelastline = false;
        static int defscreenwidth = Convert.ToInt32(Math.Round(Console.LargestWindowWidth * .25, 0, MidpointRounding.ToEven));
        static int defscreenheight = Convert.ToInt32(Math.Round(Console.LargestWindowHeight * .40, 0, MidpointRounding.ToEven));
        static int screenwidth = defscreenwidth;
        static int screenheight = defscreenheight;
        static List<string> textlines = new List<string>();

        static void Main(string[] args)
        {
            if (args.Contains("-help"))
            {
                showHelp();
            }
            else
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
                            if (!valid)
                            {
                                Console.WriteLine("Invalid Width Argument. Use -help for help");
                                return;
                            }

                        }
                        if (args[i] == "-h")
                        {
                            bool valid = Int32.TryParse(args[i + 1], out screenheight); if (!valid)
                            {
                                Console.WriteLine("Invalid Height Argument. Use -help for help");
                                return;
                            }
                        }
                        if (args[i] == "-dd")
                        {
                            bool valid = Int32.TryParse(args[i + 1], out dotdelay); if (!valid)
                            {
                                Console.WriteLine("Invalid Dot-Delay Argument. Use -help for help");
                                return;
                            }
                        }
                        if (args[i] == "-cd")
                        {
                            bool valid = Int32.TryParse(args[i + 1], out chardelay);
                            if (!valid)
                            {
                                Console.WriteLine("Invalid Character Delay Argument. Use -help for help");
                                return;
                            }
                        }
                        if (args[i] == "-ld")
                        {
                            bool valid = Int32.TryParse(args[i + 1], out linedelay);
                            if (!valid)
                            {
                                Console.WriteLine("Invalid Line Delay Argument. Use -help for help");
                                return;
                            }
                        }
                        if (args[i] == "-eld")
                        {
                            bool valid = Int32.TryParse(args[i + 1], out emptyLineDelay);
                            if (!valid)
                            {
                                Console.WriteLine("Invalid Empty Line Delay Argument. Use -help for help");
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
                            string filepath = args[i + 1];
                            FileInfo textFile = new FileInfo(filepath);
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
                        if (args[i] == "-cbl")
                        {
                            clearbeforelastline = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                if (screenwidth != defscreenwidth || screenheight != defscreenheight)
                {
                    Console.WindowWidth = screenwidth;
                    Console.WindowHeight = screenheight;
                }
                Console.Clear();
                Console.WriteLine("Press any key to begin animation");
                Console.ReadKey();

                Console.Clear();

                for (int li = 0; li < textlines.Count; li++)
                {
                    string raw = textlines[li];
                    string line = "   " + raw;
                    if (li == textlines.Count - 1 && clearbeforelastline)
                        Console.Clear();
                    if (!doublespace)
                        Console.WriteLine();
                    else Console.WriteLine(Environment.NewLine);

                    if (raw.StartsWith("!"))
                    {
                        Console.Write($"   {raw.Substring(1, raw.Length - 1)}");
                    }
                    else
                    {
                        int chars = line.Length;
                        for (int i = 1; i < chars + 1; i++)
                        {
                            string segment = line.Substring(0, i);
                            string character = segment.Substring(segment.Length - 1, 1);
                            Console.Write("\r{0}", segment);
                            if (character == "." && dotdelay > 0)
                                Thread.Sleep(dotdelay);
                            else if (i > 2) Thread.Sleep(chardelay);
                        }
                        if (string.IsNullOrEmpty(line.Replace(" ", "")))
                            Thread.Sleep(emptyLineDelay);
                        else if (randomizelinedelay)
                        {
                            Random r = new Random();
                            int ld = r.Next(0, 500);
                            int neg = r.Next(0, 2);
                            if (neg == 0)
                                ld = 0 - ld;
                            if (li < textlines.Count - 1) Thread.Sleep(linedelay + ld < 0 ? 0 : linedelay + ld);
                        }
                        else if (li < textlines.Count - 1) Thread.Sleep(linedelay);
                    }
                }
                Console.WriteLine(Environment.NewLine);
                Console.Write("   ");

                Console.ReadKey();
            }
        }

        static void showHelp()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("AnimatedTextConsole Arguments");
            Console.WriteLine();
            Console.WriteLine(" -cd  | set delay in ms between characters         | example: -cd 30   ");
            Console.WriteLine(" -dd  | set delay in ms between periods (.)        | example: -dd 300  ");
            Console.WriteLine(" -ld  | set delay in ms before next line           | example: -ld 800  ");
            Console.WriteLine(" -eld | set delay in ms for empty lines            | example: -eld 10  ");
            Console.WriteLine(" -rld | randomize line delay between by +/- 500 ms | example: -rld ");
            Console.WriteLine();
            Console.WriteLine("To bypass all above delays for one or more lines of text, begin the line with \"!\"");
            Console.WriteLine("example:");
            Console.WriteLine("!This line will bypass character and line delays");
            Console.WriteLine("!Another line that will bypass delays");
            Console.WriteLine();
            Console.WriteLine("Example: !This line will bypass character and line delays");
            Console.WriteLine(" -ds  | enable double spacing between lines        | example: -ds ");
            Console.WriteLine(" -txt | set txt filepath with lines to display     | example: -txt \"" + @"c:\users\bobdole\downloads\textlines.txt" + "\" ");
            Console.WriteLine(" -w   | set window width in columns                | example: -w 50 ");
            Console.WriteLine(" -h   | set window height in columns               | example: -h 50 ");
            Console.WriteLine(" -cbl | clear console before last line of text     | example: -cbl ");
            Console.ReadKey();
        }
    }
}
