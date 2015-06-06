﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
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
using System.Threading.Tasks;
using raytracer.filters;

namespace rt
{
    public partial class RayTracer : Form
    {
        private Bitmap _origin;
        private bool _filtersState;
        private string _file;
        private const uint NSamples = 1;
        private readonly Scene _scene;
        private readonly MyFilm _film;
        private readonly Renderer _renderer;

        public RayTracer()
        {
            _filtersState = false;
            InitializeComponent();
            _scene = new Scene();
            var screen = new Screen(1024, 768);
            _film = new MyFilm(screen, NSamples);
            Camera camera = new SimpleCamera(screen,
                Transformation.Translation(400, 400, -1000));
            _renderer = new Renderer(_scene,
                new GridSampler(screen), camera, _film,
                new WhittedIntegrator());
            _scene.Lights.Add(new PointLight(Transformation.Translation(0, 200, -500)));
            _scene.Lights.Add(new PointLight(Transformation.Translation(300, 200, -600)));
            _scene.Lights.Add(new PointLight(Transformation.Translation(500, 000, -300)));
        }

        public async void Render()
        {
            _scene.Initialize();
            var elapsed = await Task.Run(() => _renderer.Render());
            StatusText.ForeColor = System.Drawing.Color.FromArgb((int)0x40, (int)0x40, (int)0x40);
            StatusText.Text = "Rendered in " + (elapsed / 1000f).ToString("F3") + " seconds.";
            _film.Display(RenderPicture);
            _origin = new Bitmap(RenderPicture.Image);
            PathText.ForeColor = System.Drawing.Color.FromArgb((int)0xFF, (int)0x61, (int)0x61);
            PathText.Text = "No file selected.";
            _file = null;
            LoadButton.Enabled = true;
            if (_filtersState == false)
                SwitchFiltersState();
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

            public override void AddSample(Sample sample, SampledSpectrum spectrum)
            {
                var color = Colors[(int)sample.Y, (int)sample.X];
                if (color != null)
                    color.AddSample(spectrum.ToRGB());
            }
        }

        public class MyImage : IImage
        {
            private Bitmap _bitmap;

            public MyImage(Bitmap _image)
            {
                _bitmap = _image;
            }

            public Int32 GetPixel(uint x, uint y)
            {
                return _bitmap.GetPixel((int)x, (int)y).ToArgb();
            }

            public void PutPixel(uint x, uint y, int color)
            {
                _bitmap.SetPixel((int)x, (int)y, Color.FromArgb(color));
            }

            public uint XLimit
            {
                get
                {
                    return (uint)_bitmap.Width;
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public uint YLimit
            {
                get
                {
                    return (uint)_bitmap.Height;
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult File = openFileDialog1.ShowDialog();
            if (File == DialogResult.OK)
            {
                _file = openFileDialog1.FileName;
                string extension = Path.GetExtension(_file);
                if (!_file.EndsWith(".obj"))
                {
                    RenderButton.Enabled = false;
                    PathText.ForeColor = System.Drawing.Color.FromArgb((int)0xFF, (int)0x61, (int)0x61);
                    PathText.Text = "No file selected.";
                    StatusText.ForeColor = System.Drawing.Color.FromArgb((int)0xFF, (int)0x61, (int)0x61);
                    StatusText.Text = "Please select a valid filename (.obj).";
                    return ;
                }
                PathText.ForeColor = System.Drawing.Color.FromArgb((int)0x40, (int)0x40, (int)0x40);
                PathText.Text = _file;
                StatusText.ForeColor = System.Drawing.Color.FromArgb((int)0x2E, (int)0xCC, (int)0x71);
                StatusText.Text = "Ready to render the scene.";
                RenderButton.Enabled = true;
            }
        }

        private void RenderButton_Click(object sender, EventArgs e)
        {
            if (_filtersState == true)
                SwitchFiltersState();
            LoadButton.Enabled = false;
            RenderButton.Enabled = false;
            if (RenderPicture.Image != null)
                RenderPicture.Image = null;
            StatusText.ForeColor = System.Drawing.Color.FromArgb((int)0x2E, (int)0xCC, (int)0x71);
            StatusText.Text = "Rendering in progress...";
            SimpleObjParser(_scene, _file);
            Render();
        }

        private void SwitchFiltersState()
        {
            _filtersState = !_filtersState;
            FiltersContrastMore.Enabled = _filtersState;
            FiltersBorderEnhancement.Enabled = _filtersState;
            FiltersBlur.Enabled = _filtersState;
            FiltersBorderDetect.Enabled = _filtersState;
            FiltersBorderDetectMore.Enabled = _filtersState;
            FiltersPush.Enabled = _filtersState;
            FiltersSharpeness.Enabled = _filtersState;
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
                System.Windows.Forms.Application.Exit();
            else
                System.Environment.Exit(1);
        }

        private void FiltersContrastMore_Click(object sender, EventArgs e)
        {
            _filtersState = true;
            LoadButton.Enabled = false;
            RenderButton.Enabled = false;
            SwitchFiltersState();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new ContrastMore(new MyImage(_origin), new MyImage(result));
            RenderPicture.Image = result;
            SwitchFiltersState();
            FiltersContrastMore.Enabled = false;
            LoadButton.Enabled = true;
            if (_file != null)
                RenderButton.Enabled = true;
        }

        private void FiltersBorderEnhancement_Click(object sender, EventArgs e)
        {
            _filtersState = true;
            LoadButton.Enabled = false;
            RenderButton.Enabled = false;
            SwitchFiltersState();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new BorderMore(new MyImage(_origin), new MyImage(result));
            RenderPicture.Image = result;
            SwitchFiltersState();
            FiltersBorderEnhancement.Enabled = false;
            LoadButton.Enabled = true;
            if (_file != null)
                RenderButton.Enabled = true;
        }

        private void FiltersBlur_Click(object sender, EventArgs e)
        {
            _filtersState = true;
            LoadButton.Enabled = false;
            RenderButton.Enabled = false;
            SwitchFiltersState();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new Blur(new MyImage(_origin), new MyImage(result));
            RenderPicture.Image = result;
            SwitchFiltersState();
            FiltersBlur.Enabled = false;
            LoadButton.Enabled = true;
            if (_file != null)
                RenderButton.Enabled = true;
        }

        private void FiltersBorderDetect_Click(object sender, EventArgs e)
        {
            _filtersState = true;
            LoadButton.Enabled = false;
            RenderButton.Enabled = false;
            SwitchFiltersState();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new BorderDetect(new MyImage(_origin), new MyImage(result));
            RenderPicture.Image = result;
            SwitchFiltersState();
            FiltersBorderDetect.Enabled = false;
            LoadButton.Enabled = true;
            if (_file != null)
                RenderButton.Enabled = true;
        }

        private void FiltersBorderDetectMore_Click(object sender, EventArgs e)
        {
            _filtersState = true;
            LoadButton.Enabled = false;
            RenderButton.Enabled = false;
            SwitchFiltersState();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new BorderDetectMore(new MyImage(_origin), new MyImage(result));
            RenderPicture.Image = result;
            SwitchFiltersState();
            FiltersBorderDetectMore.Enabled = false;
            LoadButton.Enabled = true;
            if (_file != null)
                RenderButton.Enabled = true;
        }

        private void FiltersPush_Click(object sender, EventArgs e)
        {
            _filtersState = true;
            LoadButton.Enabled = false;
            RenderButton.Enabled = false;
            SwitchFiltersState();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new Push(new MyImage(_origin), new MyImage(result));
            RenderPicture.Image = result;
            SwitchFiltersState();
            FiltersPush.Enabled = false;
            LoadButton.Enabled = true;
            if (_file != null)
                RenderButton.Enabled = true;
        }

        private void FiltersSharpeness_Click(object sender, EventArgs e)
        {
            _filtersState = true;
            LoadButton.Enabled = false;
            RenderButton.Enabled = false;
            SwitchFiltersState();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new Sharpen(new MyImage(_origin), new MyImage(result));
            RenderPicture.Image = result;
            SwitchFiltersState();
            FiltersSharpeness.Enabled = false;
            LoadButton.Enabled = true;
            if (_file != null)
                RenderButton.Enabled = true;
        }
    }
}