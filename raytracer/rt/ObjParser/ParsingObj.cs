using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.IO;
using OpenTK;
using raytracer.core;

namespace rt.ObjParser
{
    public class ParsingObj
    {
        private string[] _objLines;
        private string _mtlPath;
        private List<Vector3> _vertexList = new List<Vector3>(); 
        private Dictionary<string, FacesGroup> _facesGroup = new Dictionary<string, FacesGroup>();
        private Dictionary<string, MaterialsGroup> _materialsProperty = new Dictionary<string, MaterialsGroup>();
        
        /// <summary>
        /// ParsingObj constructor
        /// </summary>
        /// <param name="objPath">Path of the obj file</param>
        public ParsingObj(string objPath)
        {
            _objLines = File.ReadAllLines(objPath);
            _mtlPath = _getMtlPath(objPath);
            _materialsProperty.Add("default", new MaterialsGroup("default"));
            if (_mtlPath != null)
            {
                ParsingMtl mtlParsing = new ParsingMtl(_mtlPath, _materialsProperty);
                mtlParsing.ParseMaterials();
                mtlParsing.ParsingDisplay();
            }
            _getVertexList();
            _getGroups();
        }

        public void AddToScene(Scene scene)
        {
            foreach (var group in _facesGroup.Values)
            {
                var triangleMesh = group.ExportToTriangleMesh(_vertexList);
                var material = _materialsProperty["default"].ExportToMaterial();
                if (_materialsProperty.ContainsKey(group.Material))
                    material = _materialsProperty[group.Material].ExportToMaterial();
                var primitive = new Primitive(triangleMesh, material);
                scene.Elements.Add(primitive);
            }
        }

        /// <summary>
        /// Get the mtl path
        /// </summary>
        /// <param name="objPath">Path of the obj file</param>
        /// <returns></returns>
        private string _getMtlPath(string objPath)
        {
            Regex rgxMtl = new Regex(@"mtllib\s+(\w+\.mtl)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (string line in _objLines)
            {
                if (rgxMtl.IsMatch(line))
                     return Path.GetDirectoryName(objPath) + @"\" + rgxMtl.Match(line).Groups[1].Value;
            }
            return null;
        }

        /// <summary>
        /// Get the vertex list
        /// </summary>
        private void _getVertexList()
        {
            Regex rgxVertex = new Regex(@"v\s+([+|-]?\d+(?:\.\d+)?)\s+([+|-]?\d+(?:\.\d+)?)\s+([+|-]?\d+(?:\.\d+)?)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (string line in _objLines)
            {
                if (rgxVertex.IsMatch(line))
                {
                    _vertexList.Add(new Vector3(
                        float.Parse(rgxVertex.Match(line).Groups[1].Value.Replace('.', ',')),
                        float.Parse(rgxVertex.Match(line).Groups[2].Value.Replace('.', ',')),
                        float.Parse(rgxVertex.Match(line).Groups[3].Value.Replace('.', ','))));
                }
            }
        }

        /// <summary>
        /// Get the groups in a dictionary
        /// </summary>
        private void _getGroups()
        {
            string currentGroup = "default";
            Regex rgxGroup = new Regex(@"g\s+(\w+)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex rgxFace = new Regex(@"f\s+(\d+)\s+(\d+)\s+(\d+)",
               RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex rgxMaterial = new Regex(@"usemtl\s+(\w+)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            
            _facesGroup.Add(currentGroup, new FacesGroup());
            foreach (string line in _objLines)
            {
                if (rgxGroup.IsMatch(line))
                {
                    currentGroup = rgxGroup.Match(line).Groups[1].Value;
                    if (!_facesGroup.ContainsKey(currentGroup))
                        _facesGroup.Add(currentGroup, new FacesGroup());
                }
                _getFaces(line, currentGroup, rgxFace);
                _getMaterials(line, currentGroup, rgxMaterial);
            }
        }

        /// <summary>
        /// Get the material
        /// </summary>
        /// <param name="line">Line of the obj file</param>
        /// <param name="currentGroup">Current group</param>
        /// <param name="rgxMaterial">Regex of the materials</param>
        private void _getMaterials(string line, string currentGroup, Regex rgxMaterial)
        {
            if (rgxMaterial.IsMatch(line))
                _facesGroup[currentGroup].AddMaterialName(rgxMaterial.Match(line).Groups[1].Value);
        }
        
        /// <summary>
        /// Get the face
        /// </summary>
        /// <param name="line">Line of the obj file</param>
        /// <param name="currentGroup">Current group</param>
        /// <param name="rgxFace">Regex of the faces</param>
        private void _getFaces(string line, string currentGroup, Regex rgxFace)
        {
            if (rgxFace.IsMatch(line))
            {
                _facesGroup[currentGroup].AddToFacesList(
                    float.Parse(rgxFace.Match(line).Groups[1].Value),
                    float.Parse(rgxFace.Match(line).Groups[2].Value),
                    float.Parse(rgxFace.Match(line).Groups[3].Value));
            }
        }

        /// <summary>
        /// Display the parsing of the obj file
        /// </summary>
        public void ParsingDisplay()
        {
            Console.WriteLine("mtllib:\n{0}\n", _mtlPath);
            Console.WriteLine("Vertex:");
            foreach (Vector3 vertex in _vertexList)
                Console.WriteLine(vertex);
            Console.Write("\n");
            foreach (KeyValuePair<string, FacesGroup> group in _facesGroup)
            {
                Console.WriteLine("Group: {0}", group.Key);
                Console.WriteLine("Material: {0}", group.Value.Material);
                foreach (Vector3 face in group.Value.FacesList)
                    Console.WriteLine(face);
                Console.Write("\n");
            }
        }
    }
}
