using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public struct OpzVector3
    {
        public float X;

        public float Y;

        public float Z;

        public OpzVector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:R}, {1:R}, {2:R}", this.X, this.Y, this.Z);
        }

        public static OpzVector3 Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            var values = s.Split(',');

            if (values.Length != 3)
            {
                throw new FormatException();
            }

            OpzVector3 v = new OpzVector3();

            v.X = float.Parse(values[0], CultureInfo.InvariantCulture);
            v.Y = float.Parse(values[1], CultureInfo.InvariantCulture);
            v.Z = float.Parse(values[2], CultureInfo.InvariantCulture);

            return v;
        }
    }
}
