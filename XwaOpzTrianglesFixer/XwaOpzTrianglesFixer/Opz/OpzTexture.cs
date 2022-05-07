using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public class OpzTexture
    {
        public OpzTexture()
        {
            this.TransparencyFilters = new List<OpzTransparencyFilter>();
            this.IlluminationFilters = new List<OpzIlluminationFilter>();
        }

        public string Name { get; set; }

        public List<OpzTransparencyFilter> TransparencyFilters { get; private set; }

        public List<OpzIlluminationFilter> IlluminationFilters { get; private set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
