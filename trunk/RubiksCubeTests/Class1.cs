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

        [Ignore]
        public void TestSerialize()
        {
            Cube c = Cube.MakeCube();
            c.Scramble();

            XmlSerializer s = new XmlSerializer(typeof(Cube));
            StringWriter w = new StringWriter();

            s.Serialize(w, c);
            w.Close();




        }
    }
}
