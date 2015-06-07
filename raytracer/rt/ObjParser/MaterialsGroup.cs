using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using OpenTK;
using raytracer.materials;
using raytracer.core;

namespace rt.ObjParser
{
    public class MaterialsGroup
    {
        public string Name { get; private set; }
        public int Illum = 1;
        public Vector3 Ka = new Vector3(1, 1, 1);
        public Vector3 Kd = new Vector3(1, 1, 1);
        public Vector3 Ks = new Vector3(1, 1, 1);
        public float Ns = 1;
        public float d = 1;

        /// <summary>
        /// MaterialsGroup constructor
        /// </summary>
        /// <param name="name">Name of the material</param>
        public MaterialsGroup(string name)
        {
            Name = name;
        }

        public void AddIllum(int nb)
        {
            Illum = nb;
        }

        public void AddKa(float x, float y, float z)
        {
            Ka.X = x;
            Ka.Y = y;
            Ka.Z = z;
        }

        public void AddKd(float x, float y, float z)
        {
            Kd.X = x;
            Kd.Y = y;
            Kd.Z = z;
        }

        public void AddKs(float x, float y, float z)
        {
            Ks.X = x;
            Ks.Y = y;
            Ks.Z = z;
        }

        public void AddNs(float nb)
        {
            Ns = nb;
        }

        public Material ExportToMaterial()
        {
            return new ClementiteMaterial(Ka, Kd, Ks, (uint)Ns, (uint)Illum);
        }
    }
}
