using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.IO;

namespace rt.ObjParser
{
    public class ParsingMtl
    {
        private string[] _lines;
        private Dictionary<string, MaterialsGroup> _materialsProperty;

        public ParsingMtl(string mtlPath, Dictionary<string, MaterialsGroup> materialsProperty)
        {
            _lines = File.ReadAllLines(mtlPath);
            _materialsProperty = materialsProperty;
        }

        public Dictionary<string, MaterialsGroup> ParseMaterials()
        {
            string currentGroup = "default";
            Regex rgxNewMtl = new Regex(@"newmtl\s+(\w+)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex rgxIllum = new Regex(@"illum\s+(\d+)", // 0 10
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex rgxKa = new Regex(@"Ka\s+([+|-]?\d+(?:\.\d+)?)\s+([+|-]?\d+(?:\.\d+)?)\s+([+|-]?\d+(?:\.\d+)?)", // 0 1 ,
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex rgxKd = new Regex(@"Kd\s+([+|-]?\d+(?:\.\d+)?)\s+([+|-]?\d+(?:\.\d+)?)\s+([+|-]?\d+(?:\.\d+)?)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex rgxKs = new Regex(@"Ks\s+([+|-]?\d+(?:\.\d+)?)\s+([+|-]?\d+(?:\.\d+)?)\s+([+|-]?\d+(?:\.\d+)?)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex rgxNs = new Regex(@"", // 0 10000 ,
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (string line in _lines)
            {
                if (rgxNewMtl.IsMatch(line))
                {
                    currentGroup = rgxNewMtl.Match(line).Groups[1].Value;
                    if (!_materialsProperty.ContainsKey(currentGroup))
                        _materialsProperty.Add(currentGroup, new MaterialsGroup(currentGroup));
                }
                _getIllum(line, currentGroup, rgxIllum);
                _getKa(line, currentGroup, rgxKa);
                _getKd(line, currentGroup, rgxKd);
                _getKs(line, currentGroup, rgxKs);
            }
            return _materialsProperty;
        }

        private void _getIllum(string line, string currentGroup, Regex rgx)
        {
            if (rgx.IsMatch(line))
            {
                _materialsProperty[currentGroup].AddIllum(
                    int.Parse(rgx.Match(line).Groups[1].Value));
            }
        }

        private void _getKa(string line, string currentGroup, Regex rgx)
        {
            if (rgx.IsMatch(line))
            {
                _materialsProperty[currentGroup].AddKa(
                    float.Parse(rgx.Match(line).Groups[1].Value.Replace('.', ',')),
                    float.Parse(rgx.Match(line).Groups[2].Value.Replace('.', ',')),
                    float.Parse(rgx.Match(line).Groups[3].Value.Replace('.', ',')));
            }
        }

        private void _getKd(string line, string currentGroup, Regex rgx)
        {
            if (rgx.IsMatch(line))
            {
                _materialsProperty[currentGroup].AddKd(
                    float.Parse(rgx.Match(line).Groups[1].Value.Replace('.', ',')),
                    float.Parse(rgx.Match(line).Groups[2].Value.Replace('.', ',')),
                    float.Parse(rgx.Match(line).Groups[3].Value.Replace('.', ',')));
            }
        }

        private void _getKs(string line, string currentGroup, Regex rgx)
        {
            if (rgx.IsMatch(line))
            {
                _materialsProperty[currentGroup].AddKs(
                    float.Parse(rgx.Match(line).Groups[1].Value.Replace('.', ',')),
                    float.Parse(rgx.Match(line).Groups[2].Value.Replace('.', ',')),
                    float.Parse(rgx.Match(line).Groups[3].Value.Replace('.', ',')));
            }
        }

        /// <summary>
        /// Display the parsing of the mtl file
        /// </summary>
        public void ParsingDisplay()
        {
            foreach (KeyValuePair<string, MaterialsGroup> group in _materialsProperty)
            {
                Console.WriteLine("Material: {0}", group.Value.Name);
                Console.WriteLine("illum: {0}", group.Value.Illum);
                Console.WriteLine("Ka: {0}", group.Value.Ka);
                Console.WriteLine("Kd: {0}", group.Value.Kd);
                Console.WriteLine("Ks: {0}", group.Value.Ks);
            }
            Console.Write("\n");
        }
    }
}
