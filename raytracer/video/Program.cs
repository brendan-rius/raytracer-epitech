using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using OpenTK;
using raytracer;
using raytracer.cameras;
using raytracer.core;
using raytracer.core.mathematics;
using raytracer.integrators;
using raytracer.lights;
using raytracer.materials;
using raytracer.samplers;
using raytracer.shapes;
using Screen = raytracer.core.Screen;
using System.Threading.Tasks;
using Accord.Extensions.Imaging;
using Accord.Collections;
using Accord.Extensions.Caching;
using Accord.Extensions.Math;

namespace video
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public class video : Form
        {
            private Bitmap _origin;
            private string _file;
            private const uint NSamples = 1;
            private Scene _scene;
            private MyFilm _film;
            private Renderer _renderer;

            public void makeAvi(string imageInputfolderName, string outVideoFileName, float fps, string imgSearchPattern = "*.jpg")
            {   // reads all images in folder 
                VideoWriter w = new VideoWriter(outVideoFileName,
                    new Accord.Extensions.Size(1024, 768), fps, true);
                Accord.Extensions.Imaging.ImageDirectoryReader ir =
                    new ImageDirectoryReader(imageInputfolderName, imgSearchPattern);
                while (ir.Position < ir.Length)
                {
                    IImage i = ir.Read();
                    w.Write(i);
                }
                w.Close();
            }

            public void Video_Render(float x, float y, float z, float orientation)
            {
                _scene = new Scene();
                var screen = new Screen(1024, 768);
                _film = new MyFilm(screen, NSamples);
                Camera camera = new SimpleCamera(screen,
                    Transformation.Translation(x, y, z) *
                    Transformation.Rotate(0, orientation, 0));
                _renderer = new Renderer(_scene,
                    new GridSampler(screen), camera, _film,
                    new WhittedIntegrator());
                _scene.Lights.Add(new PointLight(Transformation.Translation(0, 300, 0)));
                SimpleObjParser(_scene, "C:\\Users\\ouvran_a\\Desktop\\bite.obj");
                Render();
            }

            private System.Windows.Forms.PictureBox RenderPicture;
            private System.Windows.Forms.Label StatusText;

            public async void Render()
            {
                var elapsed = await Task.Run(() => _renderer.Render());
                _film.Display(RenderPicture);
                _origin = new Bitmap(RenderPicture.Image);
                _origin.Save("lol.jpg");
                _file = null;
            }

            public void Calc_camera()
            {
                float x, z, orientation, time, count, radius, angle;

                orientation = 0;
                count = 0;
                time = 300;
                x = 0;
                angle = (float)(-Math.PI / 2);
                radius = 100;
                while (count <= time)
                {
                    z = (float)(radius * Math.Sin(angle));
                    x = (float)(radius * Math.Cos(angle));
                    angle += 360 / 300;
                    orientation += 360 / 300;
                    Video_Render(x, 0, z, orientation);
                    count++;
                }
                makeAvi("", "RayTracer.wmv", 60);
            }

            public void SimpleObjParser(Scene scene, string filename)
            {
                var lines = File.ReadAllLines(filename);
                var verts = lines.Where(l => Regex.IsMatch(l, @"^v(\s+-?\d+\.?\d+([eE][-+]?\d+)?){3,3}$"))
                    .Select(l => Regex.Split(l, @"\s+", RegexOptions.None).Skip(1).ToArray())
                    .Select(
                        nums =>
                            new Vector3(float.Parse(nums[0], CultureInfo.InvariantCulture),
                                float.Parse(nums[1], CultureInfo.InvariantCulture),
                                float.Parse(nums[2], CultureInfo.InvariantCulture)))
                    .ToList();
                var triangles = lines.Where(l => Regex.IsMatch(l, @"^f(\s\d+(\/+\d+)?){3,3}$"))
                    .Select(l => Regex.Split(l, @"\s+", RegexOptions.None).Skip(1).ToArray())
                    .Select(i => i.Select(a => Regex.Match(a, @"\d+", RegexOptions.None).Value).ToArray())
                    .Select(nums =>
                    {
                        var p1 = verts.ElementAt(int.Parse(nums[0]) - 1);
                        var p2 = verts.ElementAt(int.Parse(nums[1]) - 1);
                        var p3 = verts.ElementAt(int.Parse(nums[2]) - 1);
                        return new Triangle(new Vector3[3] { p1, p2, p3 });
                    })
                    .ToList();
                scene.Elements.Add(new Primitive(new TriangleMesh(triangles), new MatteMaterial()));
            }

        }

        internal class MyFilm : Film
        {
            public MyFilm(Screen screen, uint nsamples)
                : base(screen)
            {
                Flag = new Bitmap((int)screen.Width, (int)screen.Height);
                Colors = new SampledColor[screen.Height, screen.Width];
                for (var i = 0; i < screen.Height; ++i)
                {
                    for (var j = 0; j < screen.Width; ++j)
                        Colors[i, j] = new SampledColor(nsamples);
                }
                Screen = screen;
            }

            public SampledColor[,] Colors { get; set; }
            public Bitmap Flag { get; set; }
            public PictureBox Picture { get; set; }

            public void Display(PictureBox picture)
            {
                for (var y = 0; y < Screen.Height; ++y)
                {
                    for (var x = 0; x < Screen.Width; ++x)
                    {
                        Flag.SetPixel(x, y, Colors[y, x].ToColor());
                    }
                }
                picture.Image = Flag;
            }

            internal class SampledColor
            {
                public float R, G, B;

                public SampledColor(uint nsamples)
                {
                    NumberOfSamples = nsamples;
                }

                public uint NumberOfSamples { get; set; }

                public void AddSample(Tuple<float, float, float> color)
                {
                    R += color.Item1;
                    G += color.Item2;
                    B += color.Item3;
                }

                public Color ToColor()
                {
                    return Color.FromArgb(MathHelper.Clamp((int)(R / NumberOfSamples * 255), 0, 255),
                        MathHelper.Clamp((int)(G / NumberOfSamples * 255), 0, 255),
                        MathHelper.Clamp((int)(G / NumberOfSamples * 255), 0, 255));
                }
            }
            public override void AddSample(Sample sample, SampledSpectrum spectrum)
            {
                var color = Colors[(int)sample.Y, (int)sample.X];
                if (color != null)
                    color.AddSample(spectrum.ToRGB());
            }
        }

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new video().Calc_camera();
        }
    }
}
