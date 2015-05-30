using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using raytracer.cameras;
using raytracer.core;
using raytracer.core.mathematics;
using raytracer.lights;
using raytracer.primitives;
using raytracer.samplers;
using Screen = raytracer.core.Screen;

namespace rt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var scene = new Scene();
            scene.Lights.Add(new PointLight(Transformation.Translation(-10, 0, 0)));
            scene.Lights.Add(new PointLight(Transformation.Translation(-10, -100, 0)));
            scene.Lights.Add(new PointLight(Transformation.Translation(-10, 0, -100)));
            scene.Elements.Add(new Primitive(new Sphere(Transformation.Scale(50).InverseTransformation),
                new MatteMaterial()));
            scene.Elements.Add(new Primitive(new Plane(), new MatteMaterial()));
            var screen = new Screen(1024, 768);
            var film = new MyFilm(screen);
            var camera = new SimpleCamera(screen, Transformation.Translation(0, 10, 500));
            var renderer = new Renderer(scene, new GridSampler(screen), camera, film, new WhittedIntegrator());
            var elapsed = renderer.Render();
            label1.Text = "Rendered in " + elapsed + " milliseconds";
            film.Display(pictureBox1);
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
            return Color.FromArgb((int) MathHelper.Clamp(R/NumberOfSamples*255, 0, 255),
                (int) MathHelper.Clamp(G/NumberOfSamples*255, 0, 255),
                (int) MathHelper.Clamp(G/NumberOfSamples*255, 0, 255));
        }
    }

    internal class MyFilm : Film
    {
        public MyFilm(Screen screen) : base(screen)
        {
            Flag = new Bitmap((int) screen.Width, (int) screen.Height);
            Colors = new SampledColor[screen.Height, screen.Width];
            for (var i = 0; i < screen.Height; ++i)
            {
                for (var j = 0; j < screen.Width; ++j)
                    Colors[i, j] = new SampledColor(1);
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