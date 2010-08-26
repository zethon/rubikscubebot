using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksCube
{
    public class Command
    {
        public Moves Move = Moves.None;
        public bool IsPrime = false;
        public int Count = 1;

        //public override string ToString()
        //{
        //    if (Move == Moves.None)
        //    {
        //        return string.Empty;
        //    }

        //    string strRet = string.Empty;

        //    switch (Move)
        //    {
        //        case Moves.Up:
        //            strRet = "U";
        //        break;
        //    }

        //    if (Count 

        //    return strRet;
        //}
    }
}
