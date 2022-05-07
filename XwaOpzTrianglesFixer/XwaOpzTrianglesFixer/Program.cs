using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using XwaOpzTrianglesFixer.Opz;

namespace XwaOpzTrianglesFixer
{
    class Program
    {
        static string processName;

        [STAThread]
        static void Main()
        {
            using (var process = System.Diagnostics.Process.GetCurrentProcess())
            {
                processName = process.ProcessName;
            }

            try
            {
                Console.WriteLine("XwaOpzTrianglesFixer");

                string openFileName = GetOpenFile();
                if (string.IsNullOrEmpty(openFileName))
                {
                    Console.WriteLine("Cancelled");
                    return;
                }

                Console.WriteLine("Opening " + openFileName + " ...");
                OpzFile opzFile = OpzFile.Open(openFileName);
                Console.WriteLine("Opened");

                FixQuadsTris(opzFile);
                RemoveDumpTris(opzFile);

                string saveFileName = GetSaveAsFile(openFileName);
                if (string.IsNullOrEmpty(saveFileName))
                {
                    Console.WriteLine("Cancelled");
                    return;
                }

                Console.WriteLine("Saving " + saveFileName + " ...");
                opzFile.Save(saveFileName);
                Console.WriteLine("Saved");

                Console.WriteLine("END");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), processName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        static string GetOpenFile()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".opz",
                CheckFileExists = true,
                Filter = "OPZ files (*.opz)|*.opz"
            };

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }

        static string GetSaveAsFile(string fileName)
        {
            var dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = ".opz",
                Filter = "OPZ files (*.opz)|*.opz",
                FileName = Path.GetFileName(fileName)
            };

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }

        static void FixQuadsTris(OpzFile file)
        {
            Console.WriteLine("Fix Quads Tris...");

            file.Meshes
                .AsParallel()
                .SelectMany(t => t.Lods)
                .ForAll(lod =>
                {
                    foreach (var face in lod.Faces)
                    {
                        if (IsFaceTri(face))
                        {
                            face.VertexCoord3 = face.VertexCoord0;
                            face.VertexTexCoord3 = face.VertexTexCoord0;
                            face.VertexNormal3 = face.VertexNormal0;
                        }
                    }
                });

            Console.WriteLine("Fixed");
        }

        static void RemoveDumpTris(OpzFile file)
        {
            Console.WriteLine("Remove Dump Tris...");

            file.Meshes
                .AsParallel()
                .SelectMany(t => t.Lods)
                .ForAll(lod =>
                {
                    for (int faceIndex = lod.Faces.Count - 1; faceIndex >= 0; faceIndex--)
                    {
                        if (IsDumpTri(lod.Faces[faceIndex]))
                        {
                            lod.Faces.RemoveAt(faceIndex);
                        }
                    }
                });

            Console.WriteLine("Removed");
        }

        static bool IsFaceTri(OpzFace face)
        {
            if (OpzVector3.Equals(face.VertexCoord0, face.VertexCoord3))
            {
                return true;
            }

            if (OpzVector3.Equals(face.VertexCoord1, face.VertexCoord3))
            {
                return true;
            }

            if (OpzVector3.Equals(face.VertexCoord2, face.VertexCoord3))
            {
                return true;
            }

            return false;
        }

        static bool IsDumpTri(OpzFace face)
        {
            if (!IsFaceTri(face))
            {
                return false;
            }

            if (OpzVector3.Equals(face.VertexCoord0, face.VertexCoord1))
            {
                return true;
            }

            if (OpzVector3.Equals(face.VertexCoord0, face.VertexCoord2))
            {
                return true;
            }

            if (OpzVector3.Equals(face.VertexCoord1, face.VertexCoord2))
            {
                return true;
            }

            return false;
        }
    }
}
