using System;
using System.Drawing;
using System.Windows.Forms;
using raytracer.cameras;
using raytracer.core;
using raytracer.core.mathematics;
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
            scene.Elements.Add(new Sphere());
            var screen = new Screen(1024, 768);
            var film = new MyFilm(screen);
            var camera = new SimpleCamera(screen, Transformation.Translation(0, 0, 2));
            var renderer = new Renderer(scene, new JitterGridSampler(screen), camera, film);
            renderer.Render();
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

        public void addSample(Spectrum color)
        {
            R += color.Red;
            G += color.Green;
            B += color.Blue;
        }

        public Color toColor()
        {
            return Color.FromArgb(Math.Min((int) (R/NumberOfSamples*255), 255),
                Math.Min((int) (G/NumberOfSamples*255), 255),
                Math.Min((int) (B/NumberOfSamples*255), 255));
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
                    Colors[i, j] = new SampledColor(4);
            }
            Screen = screen;
        }

        public SampledColor[,] Colors { get; set; }
        public Bitmap Flag { get; set; }
        public PictureBox Picture { get; set; }

        public override void AddSample(Sample sample, Spectrum spectrum)
        {
            var color = Colors[(int) sample.Y, (int) sample.X];
            if (color != null)
                color.addSample(spectrum);
        }

        public void Display(PictureBox picture)
        {
            for (var y = 0; y < Screen.Height; ++y)
            {
                for (var x = 0; x < Screen.Width; ++x)
                {
                    Flag.SetPixel(x, y, Colors[y, x].toColor());
                }
            }
            picture.Image = Flag;
        }
    }
}