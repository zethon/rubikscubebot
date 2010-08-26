using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

using log4net;

namespace RubiksCube
{
    [Serializable()]
    public class Cube
    {
        static ILog log = LogManager.GetLogger(typeof(Cube));

        public List<Cubicle> Cubicles;

        public static Cube MakeCube()
        {
            Cube c = new Cube();

            c.Cubicles = new List<Cubicle>();

            // create the center cubies
            c.Cubicles.Add(new Cubicle(Face.Down, FaceColor.White));
            c.Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow));
            c.Cubicles.Add(new Cubicle(Face.Front, FaceColor.Orange));
            c.Cubicles.Add(new Cubicle(Face.Back, FaceColor.Red));
            c.Cubicles.Add(new Cubicle(Face.Left, FaceColor.Green));
            c.Cubicles.Add(new Cubicle(Face.Right, FaceColor.Blue));

            // corner cubies
            c.Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Front, FaceColor.Orange, Face.Left, FaceColor.Green));
            c.Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Left, FaceColor.Green, Face.Back, FaceColor.Red));
            c.Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Back, FaceColor.Red, Face.Right, FaceColor.Blue));
            c.Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Right, FaceColor.Blue, Face.Front, FaceColor.Orange));
            c.Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Left, FaceColor.Green, Face.Front, FaceColor.Orange));
            c.Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Front, FaceColor.Orange, Face.Right, FaceColor.Blue));
            c.Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Right, FaceColor.Blue, Face.Back, FaceColor.Red));
            c.Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Back, FaceColor.Red, Face.Left, FaceColor.Green));

            // edge cubies
            c.Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Front, FaceColor.Orange));
            c.Cubicles.Add(new Cubicle(Face.Right, FaceColor.Blue, Face.Front, FaceColor.Orange));
            c.Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Front, FaceColor.Orange));
            c.Cubicles.Add(new Cubicle(Face.Left, FaceColor.Green, Face.Front, FaceColor.Orange));
            c.Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Back, FaceColor.Red));
            c.Cubicles.Add(new Cubicle(Face.Left, FaceColor.Green, Face.Back, FaceColor.Red));
            c.Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Back, FaceColor.Red));
            c.Cubicles.Add(new Cubicle(Face.Right, FaceColor.Blue, Face.Back, FaceColor.Red));
            c.Cubicles.Add(new Cubicle(Face.Left, FaceColor.Green, Face.Up, FaceColor.Yellow));
            c.Cubicles.Add(new Cubicle(Face.Right, FaceColor.Blue, Face.Up, FaceColor.Yellow));
            c.Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Left, FaceColor.Green));
            c.Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Right, FaceColor.Blue));

            return c;
        }

        public Cube()
        {
            Cubicles = new List<Cubicle>();
        }

        public IList<FaceColor> GetFaceColors(Face f)
        {
            IList<FaceColor> ret = new List<FaceColor>();

            Face xFace = f;
            Face aFace = Face.None;
            Face bFace = Face.None;
            Face cFace = Face.None;
            Face dFace = Face.None;

            switch (f)
            {
                case Face.Front:
                    aFace = Face.Left;
                    bFace = Face.Up;
                    cFace = Face.Right;
                    dFace = Face.Down;
                break;

                case Face.Back:
                    aFace = Face.Left;
                    bFace = Face.Down;
                    cFace = Face.Right;
                    dFace = Face.Up;
                break;

                case Face.Left:
                    aFace = Face.Back;
                    bFace = Face.Up;
                    cFace = Face.Front;
                    dFace = Face.Down;
                break;

                case Face.Right:
                    aFace = Face.Front;
                    bFace = Face.Up;
                    cFace = Face.Back;
                    dFace = Face.Down;
                break;

                case Face.Up:
                    aFace = Face.Left;
                    bFace = Face.Back;
                    cFace = Face.Right;
                    dFace = Face.Front;
                break;

                case Face.Down:
                    aFace = Face.Left;
                    bFace = Face.Front;
                    cFace = Face.Right;
                    dFace = Face.Back;
                break;

                default:
                    throw new ArgumentException("Invalid cube face");
            }

            ret.Add(Cubicles.FirstOrDefault(i => i.Location.Is(xFace | aFace | bFace)).Colors[xFace]);
            ret.Add(Cubicles.FirstOrDefault(i => i.Location.Is(xFace | bFace)).Colors[xFace]);
            ret.Add(Cubicles.FirstOrDefault(i => i.Location.Is(xFace | bFace | cFace)).Colors[xFace]);

            ret.Add(Cubicles.FirstOrDefault(i => i.Location.Is(xFace | aFace)).Colors[xFace]);
            ret.Add(Cubicles.FirstOrDefault(i => i.Location.Is(xFace)).Colors[xFace]);
            ret.Add(Cubicles.FirstOrDefault(i => i.Location.Is(xFace | cFace)).Colors[xFace]);

            ret.Add(Cubicles.FirstOrDefault(i => i.Location.Is(xFace | aFace | dFace)).Colors[xFace]);
            ret.Add(Cubicles.FirstOrDefault(i => i.Location.Is(xFace | dFace)).Colors[xFace]);
            ret.Add(Cubicles.FirstOrDefault(i => i.Location.Is(xFace | cFace | dFace)).Colors[xFace]);

            return ret;
        }

        private void Rotate(Rotation r, bool bClockwise)
        {
            Face aFace = Face.None;
            Face bFace = Face.None;
            Face cFace = Face.None;
            Face dFace = Face.None;

            if (r == Rotation.X)
            {
                aFace = Face.Front;
                bFace = bClockwise ? Face.Up : Face.Down;
                cFace = Face.Back;
                dFace = bClockwise ? Face.Down : Face.Up;
            }
            else if (r == Rotation.Y)
            {
                aFace = Face.Front;
                bFace = bClockwise ? Face.Left : Face.Right;
                cFace = Face.Back;
                dFace = bClockwise ? Face.Right : Face.Left;
            }
            else if (r == Rotation.Z)
            {
                aFace = Face.Left;
                bFace = bClockwise ? Face.Up : Face.Down;
                cFace = Face.Right;
                dFace = bClockwise ? Face.Down : Face.Up;
            }

            Cubicle top = Cubicles.FirstOrDefault(x => x.Location.Is(bFace));
            Cubicle back = Cubicles.FirstOrDefault(x => x.Location.Is(cFace));
            Cubicle down = Cubicles.FirstOrDefault(x => x.Location.Is(dFace));
            Cubicle front = Cubicles.FirstOrDefault(x => x.Location.Is(aFace));

            FaceColor topTemp = top.Colors[bFace];

            top.Colors[bFace] = front.Colors[aFace];
            front.Colors[aFace] = down.Colors[dFace];
            down.Colors[dFace] = back.Colors[cFace];
            back.Colors[cFace] = topTemp;

            Cubicle frontTop = Cubicles.FirstOrDefault(x => x.Location.Is(bFace | aFace));
            Cubicle backTop = Cubicles.FirstOrDefault(x => x.Location.Is(bFace | cFace));
            Cubicle backBottom = Cubicles.FirstOrDefault(x => x.Location.Is(cFace | dFace));
            Cubicle frontBottom = Cubicles.FirstOrDefault(x => x.Location.Is(dFace | aFace));

            Cubicle frontTopTemp = new Cubicle(frontTop);

            frontTop.Colors[bFace] = frontBottom.Colors[aFace];
            frontTop.Colors[aFace] = frontBottom.Colors[dFace];

            frontBottom.Colors[aFace] = backBottom.Colors[dFace];
            frontBottom.Colors[dFace] = backBottom.Colors[cFace];

            backBottom.Colors[dFace] = backTop.Colors[cFace];
            backBottom.Colors[cFace] = backTop.Colors[bFace];

            backTop.Colors[bFace] = frontTopTemp.Colors[aFace];
            backTop.Colors[cFace] = frontTopTemp.Colors[bFace];
        }

        public void Scramble()
        {
            Face[] FaceValues = (Face[])Enum.GetValues(typeof(Face));
            Random r = new Random(DateTime.Now.Millisecond);

            int iTurns = r.Next(5, 50);
            for (int i = 0; i < iTurns; i++)
            {
                int iFace = r.Next(0, FaceValues.Count());

                Face f = FaceValues[iFace];
                bool bCW = r.Next(0, 2) == 1 ? false : true;

                if (f.Is(Face.None))
                {
                    continue;
                }

                Turn(f, bCW);
            }
        }

        private void Turn(Face f, bool bClockwise)
        {
            string strMoveText = string.Empty;
            Face xFace = Face.None;
            Face aFace = Face.None;
            Face bFace = Face.None;
            Face cFace = Face.None;
            Face dFace = Face.None;

            xFace = f;

            if (f == Face.Up)
            {
                aFace = Face.Left;
                bFace = bClockwise ? Face.Back : Face.Front;
                cFace = Face.Right;
                dFace = bClockwise ? Face.Front : Face.Back;
                strMoveText = "U" + (bClockwise ? string.Empty : "'");
            }
            else if (f == Face.Down)
            {
                aFace = Face.Left;
                bFace = bClockwise ? Face.Front : Face.Back;
                cFace = Face.Right;
                dFace = bClockwise ? Face.Back : Face.Front;
                strMoveText = "D" + (bClockwise ? string.Empty : "'");
            }
            else if (f == Face.Right)
            {
                aFace = Face.Front;
                bFace = bClockwise ? Face.Up : Face.Down;
                cFace = Face.Back;
                dFace = bClockwise ? Face.Down : Face.Up;
                strMoveText = "R" + (bClockwise ? string.Empty : "'");
            }
            else if (f == Face.Left)
            {
                aFace = Face.Back;
                bFace = bClockwise ? Face.Up : Face.Down;
                cFace = Face.Front;
                dFace = bClockwise ? Face.Down : Face.Up;
                strMoveText = "L" + (bClockwise ? string.Empty : "'");
            }
            else if (f == Face.Front)
            {
                aFace = Face.Left;
                bFace = bClockwise ? Face.Up : Face.Down;
                cFace = Face.Right;
                dFace = bClockwise ? Face.Down : Face.Up;
                strMoveText = "F" + (bClockwise ? string.Empty : "'");
            }
            else if (f == Face.Back)
            {
                aFace = Face.Right;
                bFace = bClockwise ? Face.Up : Face.Down;
                cFace = Face.Left;
                dFace = bClockwise ? Face.Down : Face.Up;
                strMoveText = "B" + (bClockwise ? string.Empty : "'");
            }

            Cubicle c1 = Cubicles.FirstOrDefault(x => x.Location.Is(xFace | aFace | bFace));
            Cubicle c2 = Cubicles.FirstOrDefault(x => x.Location.Is(xFace | bFace));
            Cubicle c3 = Cubicles.FirstOrDefault(x => x.Location.Is(xFace | bFace | cFace));
            Cubicle c4 = Cubicles.FirstOrDefault(x => x.Location.Is(xFace | aFace));
            Cubicle c6 = Cubicles.FirstOrDefault(x => x.Location.Is(xFace | cFace));
            Cubicle c7 = Cubicles.FirstOrDefault(x => x.Location.Is(xFace | aFace | dFace));
            Cubicle c8 = Cubicles.FirstOrDefault(x => x.Location.Is(xFace | dFace));
            Cubicle c9 = Cubicles.FirstOrDefault(x => x.Location.Is(xFace | cFace | dFace));

            // TODO: some of these temps can be removed
            Dictionary<Face, FaceColor> c1Temp = new Dictionary<Face, FaceColor>(c1.Colors);
            Dictionary<Face, FaceColor> c2Temp = new Dictionary<Face, FaceColor>(c2.Colors);
            Dictionary<Face, FaceColor> c3Temp = new Dictionary<Face, FaceColor>(c3.Colors);
            Dictionary<Face, FaceColor> c4Temp = new Dictionary<Face, FaceColor>(c4.Colors);
            Dictionary<Face, FaceColor> c6Temp = new Dictionary<Face, FaceColor>(c6.Colors);
            Dictionary<Face, FaceColor> c7Temp = new Dictionary<Face, FaceColor>(c7.Colors);
            Dictionary<Face, FaceColor> c8Temp = new Dictionary<Face, FaceColor>(c8.Colors);
            Dictionary<Face, FaceColor> c9Temp = new Dictionary<Face, FaceColor>(c9.Colors);

            c1.Colors[xFace] = c7Temp[xFace];
            c1.Colors[aFace] = c7Temp[dFace];
            c1.Colors[bFace] = c7Temp[aFace];

            c2.Colors[xFace] = c4Temp[xFace];
            c2.Colors[bFace] = c4Temp[aFace];

            c3.Colors[xFace] = c1Temp[xFace];
            c3.Colors[bFace] = c1Temp[aFace];
            c3.Colors[cFace] = c1Temp[bFace];

            c4.Colors[xFace] = c8Temp[xFace];
            c4.Colors[aFace] = c8Temp[dFace];

            c6.Colors[xFace] = c2Temp[xFace];
            c6.Colors[cFace] = c2Temp[bFace];

            c7.Colors[xFace] = c9Temp[xFace];
            c7.Colors[dFace] = c9Temp[cFace];
            c7.Colors[aFace] = c9Temp[dFace];

            c8.Colors[xFace] = c6Temp[xFace];
            c8.Colors[dFace] = c6Temp[cFace];

            c9.Colors[xFace] = c3Temp[xFace];
            c9.Colors[dFace] = c3Temp[cFace];
            c9.Colors[cFace] = c3Temp[bFace];
        }

        public void Move(Moves m, bool bClockwise)
        {
            switch (m)
            {
                case Moves.Up:
                    Turn(Face.Up, bClockwise);
                break;

                case Moves.Down:
                    Turn(Face.Down, bClockwise);
                break;

                case Moves.Left:
                    Turn(Face.Left, bClockwise);
                break;

                case Moves.Right:
                    Turn(Face.Right, bClockwise);
                break;

                case Moves.Front:
                    Turn(Face.Front, bClockwise);
                break;

                case Moves.Back:
                    Turn(Face.Back, bClockwise);
                break;

                case Moves.x:
                    Turn(Face.Right, bClockwise);
                    Turn(Face.Left, !bClockwise);
                    Rotate(Rotation.X, bClockwise);
                break;

                case Moves.y:
                    Turn(Face.Up, bClockwise);
                    Turn(Face.Down, !bClockwise);
                    Rotate(Rotation.Y, bClockwise);
                break;

                case Moves.z:
                    Turn(Face.Front, bClockwise);
                    Turn(Face.Back, !bClockwise);
                    Rotate(Rotation.Z, bClockwise);
                break;

                case Moves.M:
                    Rotate(Rotation.X, bClockwise);
                break;

                case Moves.E:
                    Rotate(Rotation.Y, !bClockwise);
                break;

                case Moves.S:
                    Rotate(Rotation.Z, !bClockwise);
                break;
            }
        }
    }
}
