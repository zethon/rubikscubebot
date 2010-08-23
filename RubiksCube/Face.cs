﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace RubiksCube
{
    [Serializable()]
    [Flags]
    public enum Face : int
    {
       
        None = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
        Front = 16,
        Back = 32
    }
}
