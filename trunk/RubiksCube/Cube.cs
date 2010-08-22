using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace RubiksCube
{
    public class Cube
    {
        static ILog log = LogManager.GetLogger(typeof(Cube));

        public List<Cubicle> Cubicles;

        public Cube()
        {
            Cubicles = new List<Cubicle>();

            // create the center cubies
            Cubicles.Add(new Cubicle(Face.Down, FaceColor.White));
            Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow));
            Cubicles.Add(new Cubicle(Face.Front, FaceColor.Orange));
            Cubicles.Add(new Cubicle(Face.Back, FaceColor.Red));
            Cubicles.Add(new Cubicle(Face.Left, FaceColor.Green));
            Cubicles.Add(new Cubicle(Face.Right, FaceColor.Blue));

            // corner cubies
            Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Front, FaceColor.Orange, Face.Left, FaceColor.Green));
            Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Left, FaceColor.Green, Face.Back,  FaceColor.Red));
            Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Back, FaceColor.Red, Face.Right, FaceColor.Blue));
            Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Right, FaceColor.Blue, Face.Front, FaceColor.Orange));
            Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Left, FaceColor.Green, Face.Front, FaceColor.Orange));
            Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Front, FaceColor.Orange, Face.Right, FaceColor.Blue));
            Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Right, FaceColor.Blue, Face.Back, FaceColor.Red));
            Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Back, FaceColor.Red, Face.Left, FaceColor.Green));

            // edge cubies
            Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Front, FaceColor.Orange));
            Cubicles.Add(new Cubicle(Face.Right, FaceColor.Blue, Face.Front, FaceColor.Orange));
            Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Front, FaceColor.Orange));
            Cubicles.Add(new Cubicle(Face.Left, FaceColor.Green, Face.Front, FaceColor.Orange));
            Cubicles.Add(new Cubicle(Face.Up, FaceColor.Yellow, Face.Back, FaceColor.Red));
            Cubicles.Add(new Cubicle(Face.Left, FaceColor.Green, Face.Back, FaceColor.Red));
            Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Back, FaceColor.Red));
            Cubicles.Add(new Cubicle(Face.Right, FaceColor.Blue, Face.Back, FaceColor.Red));
            Cubicles.Add(new Cubicle(Face.Left, FaceColor.Green, Face.Up, FaceColor.Yellow));
            Cubicles.Add(new Cubicle(Face.Right, FaceColor.Blue, Face.Up, FaceColor.Yellow));
            Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Left, FaceColor.Green));
            Cubicles.Add(new Cubicle(Face.Down, FaceColor.White, Face.Right, FaceColor.Blue));



        }

        public List<FaceColor> GetFaceColors(Face f)
        {
            List<FaceColor> ret = new List<FaceColor>();

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

            ret.Add(Cubicles.Find(i => i.Location.Is(xFace | aFace | bFace)).Colors[xFace]);
            ret.Add(Cubicles.Find(i => i.Location.Is(xFace | bFace)).Colors[xFace]);
            ret.Add(Cubicles.Find(i => i.Location.Is(xFace | bFace | cFace)).Colors[xFace]);

            ret.Add(Cubicles.Find(i => i.Location.Is(xFace | aFace)).Colors[xFace]);
            ret.Add(Cubicles.Find(i => i.Location.Is(xFace)).Colors[xFace]);
            ret.Add(Cubicles.Find(i => i.Location.Is(xFace | cFace)).Colors[xFace]);

            ret.Add(Cubicles.Find(i => i.Location.Is(xFace | aFace | dFace)).Colors[xFace]);
            ret.Add(Cubicles.Find(i => i.Location.Is(xFace | dFace)).Colors[xFace]);
            ret.Add(Cubicles.Find(i => i.Location.Is(xFace | cFace | dFace)).Colors[xFace]);

            return ret;
        }

        public void Rotate(Rotation r)
        {
            Rotate(r, true);
        }

        public void Rotate(Rotation r, bool bClockwise)
        {
            Face aFace = Face.None;
            Face bFace = Face.None;
            Face cFace = Face.None;
            Face dFace = Face.None;

            if (r == Rotation.X)
            {
                Turn(Face.Right);
                Turn(Face.Left, false);

                aFace = Face.Front;
                bFace = Face.Up;
                cFace = Face.Back;
                dFace = Face.Down;
            }
            else if (r == Rotation.Y)
            {
                Turn(Face.Up);
                Turn(Face.Down, false);

                aFace = Face.Front;
                bFace = Face.Left;
                cFace = Face.Back;
                dFace = Face.Right;
            }
            else if (r == Rotation.Z)
            {
                Turn(Face.Front);
                Turn(Face.Back, false);

                aFace = Face.Left;
                bFace = Face.Up;
                cFace = Face.Right;
                dFace = Face.Down;
            }

            Cubicle top = Cubicles.Find(x => x.Location.Is(bFace));
            Cubicle back = Cubicles.Find(x => x.Location.Is(cFace));
            Cubicle down = Cubicles.Find(x => x.Location.Is(dFace));
            Cubicle front = Cubicles.Find(x => x.Location.Is(aFace));

            FaceColor topTemp = top.Colors[bFace];

            top.Colors[bFace] = front.Colors[aFace];
            front.Colors[aFace] = down.Colors[dFace];
            down.Colors[dFace] = back.Colors[cFace];
            back.Colors[cFace] = topTemp;

            Cubicle frontTop = Cubicles.Find(x => x.Location.Is(bFace | aFace));
            Cubicle backTop = Cubicles.Find(x => x.Location.Is(bFace | cFace));
            Cubicle backBottom = Cubicles.Find(x => x.Location.Is(cFace | dFace));
            Cubicle frontBottom = Cubicles.Find(x => x.Location.Is(dFace | aFace));

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

                log.DebugFormat("{0} {1}", f.ToString(), bCW);
                Turn(f, bCW);
            }
        }

        public void Turn(Face f)
        {
            Turn(f, true);
        }

        public void Turn(Face f, bool bClockwise)
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

            log.InfoFormat("({0}) Rotating Face.{1} {2}", strMoveText,
                        f.ToString(), bClockwise ? "clockwise" : "counter-clockwise");

            Cubicle c1 = Cubicles.Find(x => x.Location.Is(xFace | aFace | bFace));
            Cubicle c2 = Cubicles.Find(x => x.Location.Is(xFace | bFace));
            Cubicle c3 = Cubicles.Find(x => x.Location.Is(xFace | bFace | cFace));
            Cubicle c4 = Cubicles.Find(x => x.Location.Is(xFace | aFace));
            Cubicle c6 = Cubicles.Find(x => x.Location.Is(xFace | cFace));
            Cubicle c7 = Cubicles.Find(x => x.Location.Is(xFace | aFace | dFace));
            Cubicle c8 = Cubicles.Find(x => x.Location.Is(xFace | dFace));
            Cubicle c9 = Cubicles.Find(x => x.Location.Is(xFace | cFace | dFace));

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
    }
}
