using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksCube
{
    public sealed class Parser
    {
        private int _index = -1;
        private IList<Command> _commands;
        public IList<Command> Commands
        {
            get
            {
                return _commands;
            }
        }

        private IDictionary<string, OpCodes> _commandMap;

        public Parser(IList<object> tokens)
        {
            _commandMap = new Dictionary<string, OpCodes>();
            _commandMap.Add("U", OpCodes.Up);
            _commandMap.Add("D", OpCodes.Down);
            _commandMap.Add("L", OpCodes.Left);
            _commandMap.Add("R", OpCodes.Right);
            _commandMap.Add("F", OpCodes.Front);
            _commandMap.Add("B", OpCodes.Back);
            _commandMap.Add("X", OpCodes.X);
            _commandMap.Add("Y", OpCodes.Y);
            _commandMap.Add("Z", OpCodes.Z);

            _commands = new List<Command>();
            Scan(tokens);
        }

        private void Scan(IList<object> tokens)
        {
            foreach (object o in tokens)
            {
                if (o is string)
                {
                    string strTemp = o as string;

                    if (_commandMap.ContainsKey(strTemp))
                    {
                        _index++;
                        _commands.Add(new Command { OpCode = _commandMap[strTemp] });
                    }
                    else
                    {
                        throw new Exception("Unknown command '" + strTemp + "'");
                    }
                }
                else if (o == Scanner.Prime)
                {
                    _commands[_index].IsPrime = !_commands[_index].IsPrime;
                }
                else if (o is int)
                {
                    int? i = o as int?;
                    _commands[_index].Count = (int)o;
                }
            }
        }
    }
}
