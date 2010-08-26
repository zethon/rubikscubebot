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

        private IDictionary<string, Moves> _commandMap;

        public Parser(IList<object> tokens)
        {
            _commandMap = new Dictionary<string, Moves>();
            _commandMap.Add("U", Moves.Up);
            _commandMap.Add("D", Moves.Down);
            _commandMap.Add("L", Moves.Left);
            _commandMap.Add("R", Moves.Right);
            _commandMap.Add("F", Moves.Front);
            _commandMap.Add("B", Moves.Back);
            _commandMap.Add("X", Moves.x);
            _commandMap.Add("Y", Moves.y);
            _commandMap.Add("Z", Moves.z);
            _commandMap.Add("M", Moves.M);
            _commandMap.Add("E", Moves.E);
            _commandMap.Add("S", Moves.S);

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
                        _commands.Add(new Command { Move = _commandMap[strTemp] });
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
                    Command cmd = _commands[_index];
                    cmd.Count = (int)o;
                }
            }
        }
    }
}
