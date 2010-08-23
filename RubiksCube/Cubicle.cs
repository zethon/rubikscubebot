using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RubiksCube
{
    [Serializable()]
    public class Cubicle
    {
        public Face Location;

        public SerializableDictionary<Face, FaceColor> Colors;

        public Cubicle()
        {
        }

        public Cubicle(Cubicle c)
        {
            Location = c.Location;
            Colors = new SerializableDictionary<Face, FaceColor>(c.Colors);
        }

        public Cubicle(Face zeroLoc, FaceColor zeroColor)
        {
            Colors = new SerializableDictionary<Face, FaceColor>();
            Colors.Add(zeroLoc, zeroColor);
            Location = zeroLoc;
        }

        public Cubicle(Face zeroLoc, FaceColor zeroColor, Face oneLoc, FaceColor oneColor)
        {
            Colors = new SerializableDictionary<Face, FaceColor>();
            Colors.Add(zeroLoc, zeroColor);
            Colors.Add(oneLoc, oneColor);

            Location = zeroLoc | oneLoc;
        }

        public Cubicle(Face zeroLoc, FaceColor zeroColor, Face oneLoc, FaceColor oneColor, Face twoLoc, FaceColor twoColor)
        {
            Colors = new SerializableDictionary<Face, FaceColor>();
            Colors.Add(zeroLoc, zeroColor);
            Colors.Add(oneLoc, oneColor);
            Colors.Add(twoLoc, twoColor);

            Location = zeroLoc | oneLoc | twoLoc;
        }

        public Cubicle(Face location, Dictionary<Face, FaceColor> d)
        {
            Colors = new SerializableDictionary<Face, FaceColor>(d);
            Location = location;
        }
    }
}
