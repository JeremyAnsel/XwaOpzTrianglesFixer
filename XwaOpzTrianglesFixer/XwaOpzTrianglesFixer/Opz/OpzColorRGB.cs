using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public struct OpzColorRGB
    {
        public byte R;

        public byte G;

        public byte B;

        public OpzColorRGB(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", this.R, this.G, this.B);
        }

        public static OpzColorRGB Parse(string s)
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

            OpzColorRGB c = new OpzColorRGB();

            c.R = byte.Parse(values[0], CultureInfo.InvariantCulture);
            c.G = byte.Parse(values[1], CultureInfo.InvariantCulture);
            c.B = byte.Parse(values[2], CultureInfo.InvariantCulture);

            return c;
        }
    }
}
