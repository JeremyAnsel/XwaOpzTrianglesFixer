using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public class OpzMesh
    {
        public OpzMesh()
        {
            this.Lods = new List<OpzLod>();
            this.Hardpoints = new List<OpzHardpoint>();
            this.EngineGlows = new List<OpzEngineGlow>();
        }

        public string Name { get; set; }

        public int HitzoneType { get; set; }

        public int HitzoneExplosionType { get; set; }

        public OpzVector3 HitzoneSpan { get; set; }

        public OpzVector3 HitzoneCenter { get; set; }

        public OpzVector3 HitzoneMin { get; set; }

        public OpzVector3 HitzoneMax { get; set; }

        public int HitzoneTargetId { get; set; }

        public OpzVector3 HitzoneTarget { get; set; }

        public OpzVector3 RotationPivot { get; set; }

        public OpzVector3 RotationAxis { get; set; }

        public OpzVector3 RotationAim { get; set; }

        public OpzVector3 RotationDegree { get; set; }

        public List<OpzLod> Lods { get; private set; }

        public List<OpzHardpoint> Hardpoints { get; private set; }

        public List<OpzEngineGlow> EngineGlows { get; private set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
