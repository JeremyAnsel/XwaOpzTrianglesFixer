using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public class OpzLod
    {
        public OpzLod()
        {
            this.Faces = new List<OpzFace>();
        }

        public string Name { get; set; }

        public float CloakingDistance { get; set; }

        public List<OpzFace> Faces { get; private set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
