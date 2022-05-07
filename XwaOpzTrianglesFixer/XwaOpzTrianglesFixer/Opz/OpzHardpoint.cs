using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public class OpzHardpoint
    {
        public string Name { get; set; }

        public int HardpointType { get; set; }

        public OpzVector3 Position { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
