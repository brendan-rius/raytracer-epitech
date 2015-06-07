using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using raytracer.shapes;

namespace rt.ObjParser
{
    public class FacesGroup
    {
        public string Material { get; private set; }
        public List<Vector3> FacesList { get; private set; }

        /// <summary>
        /// FacesGroup constructor
        /// </summary>
        public FacesGroup()
        {
            Material = "default";
            FacesList = new List<Vector3>();
        }

        /// <summary>
        /// Add a new material name in the current group
        /// </summary>
        /// <param name="materialName">Material name</param>
        public void AddMaterialName(string materialName)
        {
            Material = materialName;
        }


        /// <summary>
        /// Add a face in FacesList
        /// </summary>
        /// <param name="x">Vector3.X</param>
        /// <param name="y">Vector3.Y</param>
        /// <param name="z">Vector3.Z</param>
        public void AddToFacesList(float x, float y, float z)
        {
            FacesList.Add(new Vector3(x, y, z));
        }

        public TriangleMesh ExportToTriangleMesh(List<Vector3> vertices)
        {
            var triangles = new List<Triangle>();
            foreach (var ids in FacesList)
            {
                var p1 = vertices.ElementAt((int) ids.X - 1);
                var p2 = vertices.ElementAt((int) ids.Y - 1);
                var p3 = vertices.ElementAt((int) ids.Z - 1);
                triangles.Add(new Triangle(new[] { p1, p2, p3 }));
            }
            return new TriangleMesh(triangles);
        }
    }
}

//new Triangle(new Vector3[]{p1, p2, p3})
//new TriangleMesh = new TriangleMesh()