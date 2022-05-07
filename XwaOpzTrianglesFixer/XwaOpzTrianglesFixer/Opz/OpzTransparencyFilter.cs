using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public class OpzTransparencyFilter
    {
        public OpzColorRGB Color { get; set; }

        public byte Tolerance { get; set; }

        public byte Opacity { get; set; }
    }
}
