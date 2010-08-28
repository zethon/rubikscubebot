using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksCube
{
    public class Command
    {
        public Moves Move = Moves.None;

        private bool _isPrime = false;

        public bool IsPrime
        {
            get { return _isPrime; }
            set
            {
                _isPrime = value;
                Normalize();
            }
        }

        private int _count = 1;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                Normalize();
            }
        }

        public override string ToString()
        {
            if (Move == Moves.None)
            {
                return string.Empty;
            }

            string strRet = Move.ToString().Substring(0,1);

            if (Count > 1)
            {
                strRet += Count.ToString();
            }

            if (IsPrime)
            {
                strRet += @"'";
            }

            return strRet;
        }

        private void Normalize()
        {
            _count = (int)_count % 4;

            if (_count == 3)
            {
                _isPrime = !_isPrime;
                _count = 1;
            }

            if (_isPrime && _count == 2)
            {
                _isPrime = false;
            }
        }
    }
}
