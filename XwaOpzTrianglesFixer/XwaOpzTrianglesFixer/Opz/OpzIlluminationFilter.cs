using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public struct OpzIlluminationFilter
    {
        public OpzColorRGB Color { get; set; }

        public byte Tolerance { get; set; }

        public byte Brightness { get; set; }
    }
}
