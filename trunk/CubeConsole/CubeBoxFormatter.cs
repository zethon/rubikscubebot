using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RubiksCube;

namespace ConsoleApplication1
{
    class CubeBoxFormatter
    {
        public Cube Cube = null;

        public CubeBoxFormatter(Cube c)
        {
            Cube = c;
        }

        public string GetFaceBox(Face f)
        {
            string strRet = string.Empty;

            List<FaceColor> face = Cube.GetFaceColors(f);

            strRet = @"+-------+
| {0} {1} {2} |
| {3} {4} {5} |
| {6} {7} {8} |
+-------+";

            string[] strData = new string[9];
            for (int i = 0; i < 9; i++)
            {
                strData[i] = face[i].ToString().Substring(0, 1);
            }

            return string.Format(strRet,strData);
        }

        public string GetBox()
        {
            string strCube = @"
       +-------+
       | {0} {1} {2} |
       | {3} {4} {5} |
       | {6} {7} {8} |
+------+-------+------+
|{9} {10} {11} | {18} {19} {20} | {27} {28} {29}|
|{12} {13} {14} | {21} {22} {23} | {30} {31} {32}|
|{15} {16} {17} | {24} {25} {26} | {33} {34} {35}|
+------+-------+------+
       | {36} {37} {38} |
       | {39} {40} {41} |
       | {42} {43} {44} |
       +-------+
       | {45} {46} {47} |
       | {48} {49} {50} |
       | {51} {52} {53} |
       +-------+
";

            List<FaceColor> top = Cube.GetFaceColors(Face.Up);
            List<FaceColor> front = Cube.GetFaceColors(Face.Front);
            List<FaceColor> left = Cube.GetFaceColors(Face.Left);
            List<FaceColor> right = Cube.GetFaceColors(Face.Right);
            List<FaceColor> bottom = Cube.GetFaceColors(Face.Down);
            List<FaceColor> back = Cube.GetFaceColors(Face.Back);

            string[] colors = new string[54];

            for (int i = 0; i < 9; i++)
            {
                colors[i] = top[i].ToString().Substring(0, 1);
            }

            for (int i = 0; i < 9; i++)
            {
                colors[i + 9] = left[i].ToString().Substring(0, 1);
            }

            for (int i = 0; i < 9; i++)
            {
                colors[i + 18] = front[i].ToString().Substring(0, 1);
            }

            for (int i = 0; i < 9; i++)
            {
                colors[i + 27] = right[i].ToString().Substring(0, 1);
            }

            for (int i = 0; i < 9; i++)
            {
                colors[i + 36] = bottom[i].ToString().Substring(0, 1);
            }

            for (int i = 0; i < 9; i++)
            {
                colors[i + 45] = back[i].ToString().Substring(0, 1);
            }

            return string.Format(strCube, colors);
        }
    }
}
