using System;
using Collections = System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RubiksCube
{
    public sealed class Scanner
    {
        public static readonly object Prime = new object();

        private readonly Collections.IList<object> _result;
        public Collections.IList<object> Tokens
        {
            get
            {
                return _result;
            }
        }

        public Scanner(TextReader input)
        {
            _result = new Collections.List<object>();
            Scan(input);
        }

        private void Scan(TextReader input)
        {
            while (input.Peek() != -1)
            {
                char ch = (char)input.Peek();

                if (char.IsWhiteSpace(ch))
                {
                    // ignore whitespace
                    input.Read();
                }
                else if (char.IsLetter(ch))
                {
                    _result.Add(ch.ToString());
                    input.Read();
                }
                else if (char.IsDigit(ch))
                {
                    StringBuilder accum = new StringBuilder();

                    while (char.IsDigit(ch))
                    {
                        accum.Append(ch);
                        input.Read();

                        if (input.Peek() == -1)
                        {
                            break;
                        }
                        else
                        {
                            ch = (char)input.Peek();
                        }
                    }

                    _result.Add(int.Parse(accum.ToString()));
                }
                else switch (ch)
                {
                    case '\'':
                        input.Read();
                        _result.Add(Scanner.Prime);
                    break;

                    default:
                        throw new Exception("Unknown character '" + ch + "'");
                }
            }
        }
    }
}
