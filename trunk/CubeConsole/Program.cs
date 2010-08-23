using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RubiksCube;
using log4net;

namespace ConsoleApplication1
{
    class Program
    {
        static ILog log = LogManager.GetLogger(typeof(Program));

        public static T RandomEnum<T>()
        {
            T[] values = (T[])Enum.GetValues(typeof(T));
            return values[new Random(DateTime.Now.Millisecond).Next(0, values.Length)];
        }

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            if (log.IsInfoEnabled)
            {
                log.Info("Rubik's Cube console application started");
            }
            else
            {
                Console.WriteLine("Rubik's Cube console application started");
            }

            Cube cube = Cube.MakeCube();
            CubeBoxFormatter writer = new CubeBoxFormatter(cube);

            log.Info("Rubik's Cube scrambled");

            string strCommand = string.Empty;

            do
            {
                // TODO: this can be a lot prettier (ie use a command pattern and attributes)
                //       but this will do now for a quick POC

                if (log.IsInfoEnabled)
                {
                    log.Info(writer.GetBox());
                }
                else
                {
                    Console.WriteLine(writer.GetBox());
                }

                Console.WriteLine("Commands: [UDLRFBXYZ][#'], notation, scramble, solve, quit");
                Console.Write(">");

                strCommand = Console.ReadLine();

                if (strCommand.ToLower() == @"new")
                {
                    cube = new Cube();
                    writer = new CubeBoxFormatter(cube);

                    cube.Scramble();
                }
                else if (strCommand.ToLower() == @"notation")
                {
                    System.Diagnostics.Process.Start("http://www.speedsolving.com/wiki/index.php/Notation");
                }
                else if (strCommand.ToLower() == @"scramble")
                {
                    cube.Scramble();
                    writer = new CubeBoxFormatter(cube);
                }
                else if (strCommand.ToLower() == @"solve")
                {
                    cube = new Cube();
                    writer = new CubeBoxFormatter(cube);
                }
                else if (strCommand.ToLower() != "q" && strCommand.ToLower() != "quit")
                {
                    try
                    {
                        Scanner scanner = null;
                        using (StringReader reader = new StringReader(strCommand.ToUpper()))
                        {
                            scanner = new Scanner(reader);
                        }

                        Parser parser = new Parser(scanner.Tokens);
                        Generator gen = new Generator(parser.Commands);
                        gen.Execute(cube);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }
                
                log.DebugFormat("user input: {0}", strCommand);



            } while (strCommand.ToLower() != @"quit" && strCommand.ToLower() != @"q");
        }
    }
}
