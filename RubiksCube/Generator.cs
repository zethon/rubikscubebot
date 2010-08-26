using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksCube
{
    public sealed class Generator
    {
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
                }
            }
        }
    }
}
