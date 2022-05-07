using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public class OpzEngineGlow
    {
        public string Name { get; set; }

        public OpzColorRGBA InnerColor { get; set; }

        public OpzColorRGBA OuterColor { get; set; }

        public OpzVector3 Position { get; set; }

        public OpzVector3 Vector { get; set; }

        public OpzVector3 Direction1 { get; set; }

        public OpzVector3 Direction2 { get; set; }

        public OpzVector3 Direction3 { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
