using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace RubiksCube
{
    public sealed class Generator
    {
        static ILog log = LogManager.GetLogger(typeof(Generator));
        private IList<Command> _commands;

        public Generator(IList<Command> commands)
        {
            _commands = commands;
        }

        public void Execute(Cube cube)
        {
            foreach (Command cmd in _commands)
            {
                for (int i = 0; i < cmd.Count; i++)
                {
                    cube.Move(cmd.Move, !cmd.IsPrime);
                    log.DebugFormat("Executing: {0}", cmd.ToString());
                }
            }
        }
    }
}
