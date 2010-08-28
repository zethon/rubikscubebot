using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksBot
{
    class Controller
    {
        public bool AppQuit = false;

        public bool Init()
        {
            return true;
        }

        public void MainLoop()
        {
            string strInput = string.Empty;

            do
            {
                strInput = Console.ReadLine();

                if (strInput.Length == 0)
                {
                    continue;
                }

                if (strInput.ToLower() == @"quit")
                {
                    AppQuit = true;
                }
            } while (!AppQuit);
        }
    }
}
