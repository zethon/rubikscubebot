using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using NUnit.Framework;
using RubiksCube;

namespace RubiksCubeTests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestMakeCube()
        {
            Cube c = Cube.MakeCube();

            IList<FaceColor> cubicles = c.GetFaceColors(Face.Front);
            for (int i = 0; i < 9; i++)
            {
                Assert.IsTrue(cubicles[i].Is(FaceColor.Orange));
            }

            cubicles = c.GetFaceColors(Face.Back);
            for (int i = 0; i < 9; i++)
            {
                Assert.IsTrue(cubicles[i].Is(FaceColor.Red));
            }

            cubicles = c.GetFaceColors(Face.Left);
            for (int i = 0; i < 9; i++)
            {
                Assert.IsTrue(cubicles[i].Is(FaceColor.Green));
            }

            cubicles = c.GetFaceColors(Face.Right);
            for (int i = 0; i < 9; i++)
            {
                Assert.IsTrue(cubicles[i].Is(FaceColor.Blue));
            }

            cubicles = c.GetFaceColors(Face.Up);
            for (int i = 0; i < 9; i++)
            {
                Assert.IsTrue(cubicles[i].Is(FaceColor.Yellow));
            }

            cubicles = c.GetFaceColors(Face.Down);
            for (int i = 0; i < 9; i++)
            {
                Assert.IsTrue(cubicles[i].Is(FaceColor.White));
            }

        }

        [Test]
        public void TestUpTurn()
        {
            Cube c = Cube.MakeCube();
            c.Move(Moves.Up);

            IList<FaceColor> colors = c.GetFaceColors(Face.Front);

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(colors[i], FaceColor.Blue);
            }

            for (int i = 3; i < 9; i++)
            {
                Assert.AreEqual(colors[i], FaceColor.Orange);
            }
        }

        [Test]
        public void TestSerializeScambledCube()
        {
            Cube c = Cube.MakeCube();
            c.Scramble();

            XmlSerializer s = new XmlSerializer(typeof(Cube));
            TextWriter w = new StreamWriter(@"test.xml");
            
            s.Serialize(w, c);
            w.Close();

            FileStream readstream = new FileStream(@"test.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
            Cube c1 = (Cube)s.Deserialize(readstream);
            readstream.Close();

            File.Delete(@"test.xml");

            foreach (Face f in Enum.GetValues(typeof(Face)))
            {
                if (f == Face.None)
                    continue;

                IList<FaceColor> orginal = c.GetFaceColors(f);
                IList<FaceColor> loaded = c1.GetFaceColors(f);

                Assert.AreEqual(orginal, loaded);
            }
        }

        [Test]
        public void TestCommandToString()
        {
            Command c = new Command { Move = Moves.Up, Count = 1, IsPrime = false };
            Assert.AreEqual(c.ToString(), @"U");

            c = new Command { Move = Moves.Right, Count = 2, IsPrime = true };
            Assert.AreEqual(c.ToString(), @"R2");

            c = new Command { Move = Moves.Left, Count = 1, IsPrime = true };
            Assert.AreEqual(c.ToString(), @"L'");

            c = new Command { Move = Moves.x, Count = 2, IsPrime = false };
            Assert.AreEqual(c.ToString(), "x2");
        }

        [Test]
        public void TestParser()
        {
            Cube c = Cube.MakeCube();

            string strCommand = @"U2L'";
            c.ExecuteCommand(strCommand);

            IList<FaceColor> set = new List<FaceColor>();
            set.Add(FaceColor.White);
            set.Add(FaceColor.Red);
            set.Add(FaceColor.Red);
            set.Add(FaceColor.White);
            set.Add(FaceColor.Orange);
            set.Add(FaceColor.Orange);
            set.Add(FaceColor.White);
            set.Add(FaceColor.Orange);
            set.Add(FaceColor.Orange);
            IList<FaceColor> front = c.GetFaceColors(Face.Front);
            Assert.AreEqual(front, set);

            strCommand = @"x'F2UR'";
            set = new List<FaceColor>();
            //set.Add(



            
        }
    }
}
