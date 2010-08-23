using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using RubiksCube;
using log4net;

namespace RubiksBot
{
    class Program
    {
        static ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            try
            {
                Cube c = Cube.MakeCube();
                c.Scramble();
                CubeBoxFormatter f = new CubeBoxFormatter(c);
                log.Info(f.GetBox());

                XmlSerializer serializer = new XmlSerializer(typeof(Cube));
                StringWriter w = new StringWriter();

                serializer.Serialize(w, c);
                w.Close();

                StringReader reader = new StringReader(w.ToString());
                Cube c1 = (Cube)serializer.Deserialize(reader);

                CubeBoxFormatter f1 = new CubeBoxFormatter(c1);
                log.Info(f1.GetBox());
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
            }

            Console.ReadLine();
        }
    }
}
