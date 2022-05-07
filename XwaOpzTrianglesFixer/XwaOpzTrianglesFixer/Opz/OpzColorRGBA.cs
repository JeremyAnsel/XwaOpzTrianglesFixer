using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public struct OpzColorRGBA
    {
        public byte R;

        public byte G;

        public byte B;

        public byte A;

        public OpzColorRGBA(byte r, byte g, byte b, byte a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}, {3}", this.R, this.G, this.B, this.A);
        }

        public static OpzColorRGBA Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }

            var values = s.Split(',');

            if (values.Length != 4)
            {
                throw new FormatException();
            }

            OpzColorRGBA c = new OpzColorRGBA();

            c.R = byte.Parse(values[0], CultureInfo.InvariantCulture);
            c.G = byte.Parse(values[1], CultureInfo.InvariantCulture);
            c.B = byte.Parse(values[2], CultureInfo.InvariantCulture);
            c.A = byte.Parse(values[3], CultureInfo.InvariantCulture);

            return c;
        }
    }
}
