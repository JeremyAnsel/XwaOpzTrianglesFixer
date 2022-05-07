using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public class OpzFace
    {
        public string Name { get; set; }

        public OpzVector3 VertexCoord0 { get; set; }

        public OpzVector3 VertexCoord1 { get; set; }

        public OpzVector3 VertexCoord2 { get; set; }

        public OpzVector3 VertexCoord3 { get; set; }

        public OpzVector2 VertexTexCoord0 { get; set; }

        public OpzVector2 VertexTexCoord1 { get; set; }

        public OpzVector2 VertexTexCoord2 { get; set; }

        public OpzVector2 VertexTexCoord3 { get; set; }

        public OpzVector3 VertexNormal0 { get; set; }

        public OpzVector3 VertexNormal1 { get; set; }

        public OpzVector3 VertexNormal2 { get; set; }

        public OpzVector3 VertexNormal3 { get; set; }

        public OpzVector3 FaceNormal { get; set; }

        public OpzVector3 FaceVector1 { get; set; }

        public OpzVector3 FaceVector2 { get; set; }

        public string TextureNameR { get; set; }

        public string TextureNameY { get; set; }

        public string TextureNameB { get; set; }

        public string TextureNameG { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
