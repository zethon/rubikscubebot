using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksCube
{
    public class Cubicle
    {
        public Face Location;
        public Dictionary<Face, FaceColor> Colors;

        public Cubicle(Cubicle c)
        {
            Location = c.Location;
            Colors = new Dictionary<Face, FaceColor>(c.Colors);
        }

        public Cubicle(Face zeroLoc, FaceColor zeroColor)
        {
            Colors = new Dictionary<Face, FaceColor>();
            Colors.Add(zeroLoc, zeroColor);
            Location = zeroLoc;
        }

        public Cubicle(Face zeroLoc, FaceColor zeroColor, Face oneLoc, FaceColor oneColor)
        {
            Colors = new Dictionary<Face, FaceColor>();
            Colors.Add(zeroLoc, zeroColor);
            Colors.Add(oneLoc, oneColor);

            Location = zeroLoc | oneLoc;
        }

        public Cubicle(Face zeroLoc, FaceColor zeroColor, Face oneLoc, FaceColor oneColor, Face twoLoc, FaceColor twoColor)
        {
            Colors = new Dictionary<Face, FaceColor>();
            Colors.Add(zeroLoc, zeroColor);
            Colors.Add(oneLoc, oneColor);
            Colors.Add(twoLoc, twoColor);

            Location = zeroLoc | oneLoc | twoLoc;
        }

        public Cubicle(Face location, Dictionary<Face, FaceColor> d)
        {
            Colors = new Dictionary<Face, FaceColor>(d);
            Location = location;
        }
    }
}
