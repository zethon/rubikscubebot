using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            log.Info("Rubik's Cube console application started");

            Cube cube = new Cube();
            cube.Scramble();

            CubeBoxFormatter writer = new CubeBoxFormatter(cube);
            log.Debug(writer.GetBox());            

            Console.ReadLine();
        }
    }
}
