using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace XwaOpzTrianglesFixer.Opz
{
    public class OpzFile
    {
        public OpzFile()
        {
            this.Meshes = new List<OpzMesh>();
            this.Textures = new List<OpzTexture>();
        }

        public List<OpzMesh> Meshes { get; private set; }

        public List<OpzTexture> Textures { get; private set; }

        public static OpzFile Open(string filename)
        {
            using (var file = new StreamReader(filename, Encoding.ASCII))
            {
                OpzFile opz = new OpzFile();

                string version = file.ReadLine();

                if (!string.Equals(version, "v1.1"))
                {
                    throw new InvalidDataException();
                }

                int meshesCount = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                int texturesCount = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                opz.Meshes.Capacity = meshesCount;
                opz.Textures.Capacity = texturesCount;

                for (int m = 0; m < meshesCount; m++)
                {
                    OpzMesh mesh = new OpzMesh();

                    mesh.Name = file.ReadLine();

                    int lodCount = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                    int hardpointCount = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                    int engineGlowCount = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                    mesh.Lods.Capacity = lodCount;
                    mesh.Hardpoints.Capacity = hardpointCount;
                    mesh.EngineGlows.Capacity = engineGlowCount;

                    mesh.HitzoneType = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                    mesh.HitzoneExplosionType = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                    mesh.HitzoneSpan = OpzVector3.Parse(file.ReadLine());
                    mesh.HitzoneCenter = OpzVector3.Parse(file.ReadLine());
                    mesh.HitzoneMin = OpzVector3.Parse(file.ReadLine());
                    mesh.HitzoneMax = OpzVector3.Parse(file.ReadLine());
                    mesh.HitzoneTargetId = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                    mesh.HitzoneTarget = OpzVector3.Parse(file.ReadLine());
                    mesh.RotationPivot = OpzVector3.Parse(file.ReadLine());
                    mesh.RotationAxis = OpzVector3.Parse(file.ReadLine());
                    mesh.RotationAim = OpzVector3.Parse(file.ReadLine());
                    mesh.RotationDegree = OpzVector3.Parse(file.ReadLine());

                    for (int l = 0; l < lodCount; l++)
                    {
                        OpzLod lod = new OpzLod();

                        lod.Name = file.ReadLine();

                        int faceCount = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                        lod.Faces.Capacity = faceCount;

                        lod.CloakingDistance = float.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                        for (int f = 0; f < faceCount; f++)
                        {
                            OpzFace face = new OpzFace();

                            face.Name = file.ReadLine();
                            face.VertexCoord0 = OpzVector3.Parse(file.ReadLine());
                            face.VertexCoord1 = OpzVector3.Parse(file.ReadLine());
                            face.VertexCoord2 = OpzVector3.Parse(file.ReadLine());
                            face.VertexCoord3 = OpzVector3.Parse(file.ReadLine());
                            face.VertexTexCoord0 = OpzVector2.Parse(file.ReadLine());
                            face.VertexTexCoord1 = OpzVector2.Parse(file.ReadLine());
                            face.VertexTexCoord2 = OpzVector2.Parse(file.ReadLine());
                            face.VertexTexCoord3 = OpzVector2.Parse(file.ReadLine());
                            face.VertexNormal0 = OpzVector3.Parse(file.ReadLine());
                            face.VertexNormal1 = OpzVector3.Parse(file.ReadLine());
                            face.VertexNormal2 = OpzVector3.Parse(file.ReadLine());
                            face.VertexNormal3 = OpzVector3.Parse(file.ReadLine());
                            face.FaceNormal = OpzVector3.Parse(file.ReadLine());
                            face.FaceVector1 = OpzVector3.Parse(file.ReadLine());
                            face.FaceVector2 = OpzVector3.Parse(file.ReadLine());
                            face.TextureNameR = file.ReadLine();
                            face.TextureNameY = file.ReadLine();
                            face.TextureNameB = file.ReadLine();
                            face.TextureNameG = file.ReadLine();

                            lod.Faces.Add(face);
                        }

                        mesh.Lods.Add(lod);
                    }

                    for (int h = 0; h < hardpointCount; h++)
                    {
                        OpzHardpoint hardpoint = new OpzHardpoint();

                        hardpoint.Name = file.ReadLine();
                        hardpoint.HardpointType = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                        hardpoint.Position = OpzVector3.Parse(file.ReadLine());

                        mesh.Hardpoints.Add(hardpoint);
                    }

                    for (int e = 0; e < engineGlowCount; e++)
                    {
                        OpzEngineGlow engineGlow = new OpzEngineGlow();

                        engineGlow.Name = file.ReadLine();
                        engineGlow.InnerColor = OpzColorRGBA.Parse(file.ReadLine());
                        engineGlow.OuterColor = OpzColorRGBA.Parse(file.ReadLine());
                        engineGlow.Position = OpzVector3.Parse(file.ReadLine());
                        engineGlow.Vector = OpzVector3.Parse(file.ReadLine());
                        engineGlow.Direction1 = OpzVector3.Parse(file.ReadLine());
                        engineGlow.Direction2 = OpzVector3.Parse(file.ReadLine());
                        engineGlow.Direction3 = OpzVector3.Parse(file.ReadLine());

                        mesh.EngineGlows.Add(engineGlow);
                    }

                    opz.Meshes.Add(mesh);
                }

                for (int t = 0; t < texturesCount; t++)
                {
                    OpzTexture texture = new OpzTexture();

                    texture.Name = file.ReadLine();

                    int transparencyCount = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                    for (int i = 0; i < transparencyCount; i++)
                    {
                        OpzTransparencyFilter filter = new OpzTransparencyFilter();

                        filter.Color = OpzColorRGB.Parse(file.ReadLine());
                        filter.Tolerance = byte.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                        filter.Opacity = byte.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                        texture.TransparencyFilters.Add(filter);
                    }

                    int illuminationCount = int.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                    for (int i = 0; i < illuminationCount; i++)
                    {
                        OpzIlluminationFilter filter = new OpzIlluminationFilter();

                        filter.Color = OpzColorRGB.Parse(file.ReadLine());
                        filter.Tolerance = byte.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
                        filter.Brightness = byte.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

                        texture.IlluminationFilters.Add(filter);
                    }

                    opz.Textures.Add(texture);
                }

                return opz;
            }
        }

        public void Save(string filename)
        {
            using (var file = new StreamWriter(filename, false, Encoding.ASCII))
            {
                file.WriteLine("v1.1");
                file.Write(' ');
                file.WriteLine(this.Meshes.Count);
                file.Write(' ');
                file.WriteLine(this.Textures.Count);

                foreach (var mesh in this.Meshes)
                {
                    file.WriteLine(mesh.Name);
                    file.Write(' ');
                    file.WriteLine(mesh.Lods.Count);
                    file.Write(' ');
                    file.WriteLine(mesh.Hardpoints.Count);
                    file.Write(' ');
                    file.WriteLine(mesh.EngineGlows.Count);
                    file.Write(' ');
                    file.WriteLine(mesh.HitzoneType);
                    file.Write(' ');
                    file.WriteLine(mesh.HitzoneExplosionType);
                    file.WriteLine(mesh.HitzoneSpan.ToString());
                    file.WriteLine(mesh.HitzoneCenter.ToString());
                    file.WriteLine(mesh.HitzoneMin.ToString());
                    file.WriteLine(mesh.HitzoneMax.ToString());
                    file.WriteLine(mesh.HitzoneTargetId);
                    file.WriteLine(mesh.HitzoneTarget.ToString());
                    file.WriteLine(mesh.RotationPivot.ToString());
                    file.WriteLine(mesh.RotationAxis.ToString());
                    file.WriteLine(mesh.RotationAim.ToString());
                    file.WriteLine(mesh.RotationDegree.ToString());

                    foreach (var lod in mesh.Lods)
                    {
                        file.WriteLine(lod.Name);
                        file.Write(' ');
                        file.WriteLine(lod.Faces.Count);
                        file.Write(' ');
                        file.WriteLine(lod.CloakingDistance.ToString("R", CultureInfo.InvariantCulture));

                        foreach (var face in lod.Faces)
                        {
                            file.WriteLine(face.Name);
                            file.WriteLine(face.VertexCoord0.ToString());
                            file.WriteLine(face.VertexCoord1.ToString());
                            file.WriteLine(face.VertexCoord2.ToString());
                            file.WriteLine(face.VertexCoord3.ToString());
                            file.WriteLine(face.VertexTexCoord0.ToString());
                            file.WriteLine(face.VertexTexCoord1.ToString());
                            file.WriteLine(face.VertexTexCoord2.ToString());
                            file.WriteLine(face.VertexTexCoord3.ToString());
                            file.WriteLine(face.VertexNormal0.ToString());
                            file.WriteLine(face.VertexNormal1.ToString());
                            file.WriteLine(face.VertexNormal2.ToString());
                            file.WriteLine(face.VertexNormal3.ToString());
                            file.WriteLine(face.FaceNormal.ToString());
                            file.WriteLine(face.FaceVector1.ToString());
                            file.WriteLine(face.FaceVector2.ToString());
                            file.WriteLine(face.TextureNameR);
                            file.WriteLine(face.TextureNameY);
                            file.WriteLine(face.TextureNameB);
                            file.WriteLine(face.TextureNameG);
                        }
                    }

                    foreach (var hardpoint in mesh.Hardpoints)
                    {
                        file.WriteLine(hardpoint.Name);
                        file.WriteLine(hardpoint.HardpointType);
                        file.WriteLine(hardpoint.Position.ToString());
                    }

                    foreach (var engineGlow in mesh.EngineGlows)
                    {
                        file.WriteLine(engineGlow.Name);
                        file.WriteLine(engineGlow.InnerColor.ToString());
                        file.WriteLine(engineGlow.OuterColor.ToString());
                        file.WriteLine(engineGlow.Position.ToString());
                        file.WriteLine(engineGlow.Vector.ToString());
                        file.WriteLine(engineGlow.Direction1.ToString());
                        file.WriteLine(engineGlow.Direction2.ToString());
                        file.WriteLine(engineGlow.Direction3.ToString());
                    }
                }

                foreach (var texture in this.Textures)
                {
                    file.WriteLine(texture.Name);

                    file.Write(' ');
                    file.WriteLine(texture.TransparencyFilters.Count);

                    foreach (var filter in texture.TransparencyFilters)
                    {
                        file.WriteLine(filter.Color.ToString());
                        file.Write(' ');
                        file.WriteLine(filter.Tolerance);
                        file.Write(' ');
                        file.WriteLine(filter.Opacity);
                    }

                    file.Write(' ');
                    file.WriteLine(texture.IlluminationFilters.Count);

                    foreach (var filter in texture.IlluminationFilters)
                    {
                        file.WriteLine(filter.Color.ToString());
                        file.Write(' ');
                        file.WriteLine(filter.Tolerance);
                        file.Write(' ');
                        file.WriteLine(filter.Brightness);
                    }
                }
            }
        }
    }
}
