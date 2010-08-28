using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using RubiksCube;
using log4net;

namespace RubiksBot
{
    class Program
    {
        static ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();

                FileVersionInfo info = FileVersionInfo.GetVersionInfo("Rubiksbot.exe");
                string strHeader = string.Format("Starting RubiksBot {0}", info.FileVersion);

                log.Info(strHeader);

                Controller ctrl = new Controller();
                if (ctrl.Init())
                {
                    ctrl.MainLoop();
                }
                else
                {
                    log.Fatal("Could not initialize Controller object");
                }
            }
            catch (Exception ex)
            {
                log.Fatal("Main exception", ex);
            }

            /*
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

                //log.Debug(w.ToString());

                StringReader reader = new StringReader(w.ToString());
                Cube c1 = (Cube)serializer.Deserialize(reader);

                CubeBoxFormatter f1 = new CubeBoxFormatter(c1);
                log.Info(f1.GetBox());
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
            }

            Console.ReadLine();*/
        }
    }
}
