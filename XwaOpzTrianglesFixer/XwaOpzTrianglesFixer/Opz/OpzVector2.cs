using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public struct OpzVector2
    {
        public float X;

        public float Y;

        public OpzVector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:R}, {1:R}", this.X, this.Y);
        }

        public static OpzVector2 Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            var values = s.Split(',');

            if (values.Length != 2)
            {
                throw new FormatException();
            }

            OpzVector2 v = new OpzVector2();

            v.X = float.Parse(values[0], CultureInfo.InvariantCulture);
            v.Y = float.Parse(values[1], CultureInfo.InvariantCulture);

            return v;
        }
    }
}
