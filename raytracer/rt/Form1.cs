using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenTK;
using raytracer.cameras;
using raytracer.core;
using raytracer.core.mathematics;
using raytracer.integrators;
using raytracer.lights;
using raytracer.materials;
using raytracer.samplers;
using raytracer.shapes;
using Screen = raytracer.core.Screen;

namespace rt
{
    public partial class Form1 : Form
    {
        private const uint NSamples = 1;
        private readonly MyFilm _film;
        private readonly ThreadedRenderer _renderer;

        public Form1()
        {
            InitializeComponent();
            var scene = new Scene();
            var screen = new Screen(1024, 768);
            _film = new MyFilm(screen, NSamples);
            Camera camera = new SimpleCamera(screen,
                Transformation.Translation(0, 200, 1000));
            _renderer = new ThreadedRenderer(scene,
                new GridSampler(screen), camera, _film,
                new WhittedIntegrator());
            scene.Lights.Add(new PointLight(Transformation.Translation(0, 500, 500)));
            scene.Elements.Add(new Primitive(new Plane(), new MatteMaterial()));
            scene.Elements.Add(new Primitive(new Plane(Transformation.RotateX(90)), new MatteMaterial()));
            scene.Elements.Add(new Primitive(new Plane(Transformation.RotateZ(90)*Transformation.Translation(-600, 0, 0)), new MatteMaterial()));
            scene.Elements.Add(new Primitive(new Plane(Transformation.RotateZ(90)*Transformation.Translation(600, 0, 0)), new MatteMaterial()));
            Render();
        }

        public async void Render()
        {
            var elapsed = await _renderer.RenderAsync();
            label1.Text = "Rendered in " + elapsed + " milliseconds";
            _film.Display(pictureBox1);
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
            lines.Where(l => Regex.IsMatch(l, @"^f(\s\d+(\/+\d+)?){3,3}$"))
                .Select(l => Regex.Split(l, @"\s+", RegexOptions.None).Skip(1).ToArray())
                .Select(i => i.Select(a => Regex.Match(a, @"\d+", RegexOptions.None).Value).ToArray())
                .Select(nums =>
                {
                    var p1 = verts.ElementAt(int.Parse(nums[0]) - 1);
                    var p2 = verts.ElementAt(int.Parse(nums[1]) - 1);
                    var p3 = verts.ElementAt(int.Parse(nums[2]) - 1);
                    return new Triangle(new Vector3[3] {p1, p2, p3});
                })
                .ToList()
                .ForEach(p => scene.Elements.Add(new Primitive(p, new MatteMaterial())));
        }
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
            return Color.FromArgb(MathHelper.Clamp((int) (R/NumberOfSamples*255), 0, 255),
                MathHelper.Clamp((int) (G/NumberOfSamples*255), 0, 255),
                MathHelper.Clamp((int) (G/NumberOfSamples*255), 0, 255));
        }
    }

    internal class MyFilm : Film
    {
        public MyFilm(Screen screen, uint nsamples) : base(screen)
        {
            Flag = new Bitmap((int) screen.Width, (int) screen.Height);
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

        public override void AddSample(Sample sample, SampledSpectrum spectrum)
        {
            var color = Colors[(int) sample.Y, (int) sample.X];
            if (color != null)
                color.AddSample(spectrum.ToRGB());
        }
    }
}