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
                object predicate = new object();

                switch (cmd.OpCode)
                {
                    case OpCodes.Up:
                        predicate = Face.Up;
                    break;

                    case OpCodes.Down:
                        predicate = Face.Down;
                    break;

                    case OpCodes.Left:
                        predicate = Face.Left;
                    break;

                    case OpCodes.Right:
                        predicate = Face.Right;
                    break;

                    case OpCodes.Front:
                        predicate = Face.Front;
                    break;

                    case OpCodes.Back:
                        predicate = Face.Back;
                    break;

                    case OpCodes.X:
                        predicate = Rotation.X;
                    break;

                    case OpCodes.Y:
                        predicate = Rotation.Y;
                    break;

                    case OpCodes.Z:
                        predicate = Rotation.Z;
                    break;
                }

                if (predicate.GetType() == typeof(Face))
                {
                    for (int i = 0; i < cmd.Count; i++)
                    {
                        cube.Turn((Face)predicate, !cmd.IsPrime);
                    }
                }
                else if (predicate.GetType() == typeof(Rotation))
                {
                    for (int i = 0; i < cmd.Count; i++)
                    {
                        cube.Rotate((Rotation)predicate, !cmd.IsPrime);
                    }
                }
                else
                {
                    throw new Exception("Unknown predicate type '" + predicate.ToString() + "'");
                }
            }
        }
    }
}
