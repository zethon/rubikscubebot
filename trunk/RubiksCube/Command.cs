using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksCube
{
    public enum OpCodes
    {
        Null,
        Up,
        Down,
        Left,
        Right,
        Front,
        Back,
        X,
        Y,
        Z
    }

    public class Command
    {
        public OpCodes OpCode = OpCodes.Null;
        public bool IsPrime = false;
        public int Count = 1;

        public override string ToString()
        {
            if (OpCode == OpCodes.Null)
            {
                return string.Empty;
            }

            string strRet = string.Empty;

            switch (this.OpCode)
            {
                case OpCodes.Up:
                    strRet = "U";
                break;



            return strRet;
        }
    }
}
