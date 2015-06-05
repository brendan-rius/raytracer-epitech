using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using OpenTK;
using raytracer.core;
using raytracer.materials;
using raytracer.shapes;

namespace test
{
    public class ObjParser
    {
        private string _content;

        private Regex _regexPoint = new Regex(@"^\s*v\s+(-?\d+(?:\.\d*)?)\s+(-?\d+(?:\.\d*)?)\s+(-?\d+(?:\.\d*)?)\s*$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private Regex _regexGroup = new Regex(@"g\s+(\w+)(?:\s+f\s+(\d+)\s+(\d+)\s+(\d+))+",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ObjParser(string filename)
        {
            _content = File.ReadAllText(filename);
        }

        public void AddToScene(Scene scene)
        {
            var groups = ParseGroups();
            foreach (var triangleMesh in groups)
            {
                scene.Elements.Add(new Primitive(triangleMesh, new MatteMaterial()));
            }
        }

        private Dictionary<string, TriangleMesh> ParseGroups()
        {
            var points = ParsePoints();
            var groups = new Dictionary<string, TriangleMesh>();

            string file = "";
        }

        private List<Vector3> ParsePoints()
        {
            var points = new List<Vector3>();

            foreach (var line in _content)
            {
                var matches =_regexPoint.Matches(line);
                if (matches.Count == 0)
                    continue;
                var groups = matches[1].Groups;
                points.Add(new Vector3(
                    float.Parse(groups[0].Value),
                    float.Parse(groups[1].Value),
                    float.Parse(groups[2].Value)));
            }
            return points;
        }
    }
}