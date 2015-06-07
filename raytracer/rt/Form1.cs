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
using raytracer.filters;
using Screen = raytracer.core.Screen;
using System.Threading.Tasks;

namespace rt
{
    public partial class RayTracer : Form
    {
        private bool _filtersState = false;
        private string _file;
        private const uint NSamples = 1;
        private Bitmap _picture;
        private Renderer _renderer;
        private Scene _scene;
        private Screen _screen;
        private MyFilm _film;

        public RayTracer()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     This function is called to create a new scene to render.
        /// </summary>
        private void InitNewScene()
        {
            _screen = new Screen(1024, 768);
            _film = new MyFilm(_screen, NSamples);
            _scene = new Scene();
            Camera camera = new SimpleCamera(_screen,
                Transformation.Compose(
                    Transformation.Translation((float) PositionX.Value, (float) PositionY.Value, (float) PositionZ.Value),
                    Transformation.RotateX((float) RotationX.Value % 360f),
                    Transformation.RotateY((float) RotationY.Value % 360f),
                    Transformation.RotateZ((float) RotationZ.Value % 360f)
                ));
            _renderer = new Renderer(_scene, new GridSampler(_screen), camera, _film, new WhittedIntegrator());
            _scene.Lights.Add(new PointLight(Transformation.Translation(0, 0, -500)));
            _scene.Elements.Add(new Primitive(new Plane(Transformation.RotateX(90)), new MatteMaterial()));
            _scene.Elements.Add(new Primitive(new Plane(Transformation.Translation(0, 300, 0)), new MatteMaterial()));
            _scene.Elements.Add(new Primitive(new Plane(Transformation.RotateZ(90) * Transformation.Translation(-600, 0, 0)),
                new MatteMaterial()));
            _scene.Elements.Add(new Primitive(new Plane(Transformation.RotateZ(90) * Transformation.Translation(600, 0, 0)),
                new MatteMaterial()));
        }

        private async void Render()
        {
            var elapsed = await Task.Run(() => _renderer.Render());
            StatusText.ForeColor = System.Drawing.Color.FromArgb((int)0x40, (int)0x40, (int)0x40);
            StatusText.Text = "Rendered in " + (elapsed / 1000f).ToString("F3") + " seconds.";
            _film.Display(RenderPicture);
            _picture = new Bitmap(RenderPicture.Image);
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
            scene.Elements.Add(new Primitive(new TriangleMesh(triangles), new ReflectiveMaterial()));
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

        /// <summary>
        ///     This function is called when the 'exit' button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
                System.Windows.Forms.Application.Exit();
            else
                System.Environment.Exit(1);
        }

        /// <summary>
        ///     This function is called when the 'load' button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadButton_Click(object sender, EventArgs e)
        {
            DialogResult File = openFileDialog1.ShowDialog();
            if (File == DialogResult.OK)
            {
                _file = openFileDialog1.FileName;
                if (!_file.EndsWith(".obj"))
                {
                    RenderButton.Enabled = false;
                    PathText.ForeColor = System.Drawing.Color.FromArgb((int)0xFF, (int)0x61, (int)0x61);
                    PathText.Text = "No file selected.";
                    StatusText.ForeColor = System.Drawing.Color.FromArgb((int)0xFF, (int)0x61, (int)0x61);
                    StatusText.Text = "Please select a valid filename (.obj).";
                    return ;
                }
                RenderButton.Enabled = true;
                PathText.ForeColor = System.Drawing.Color.FromArgb((int)0x40, (int)0x40, (int)0x40);
                PathText.Text = _file;
                StatusText.ForeColor = System.Drawing.Color.FromArgb((int)0x2E, (int)0xCC, (int)0x71);
                StatusText.Text = "Ready to render the scene.";
            }
        }

        /// <summary>
        ///     This function is called when the 'render' button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenderButton_Click(object sender, EventArgs e)
        {
            if (_filtersState == true)
                SwitchFiltersState();
            LoadButton.Enabled = false;
            RenderButton.Enabled = false;
            RenderPicture.Image = null;
            StatusText.ForeColor = System.Drawing.Color.FromArgb((int)0x2E, (int)0xCC, (int)0x71);
            StatusText.Text = "Rendering in progress...";
            InitNewScene();
            SimpleObjParser(_scene, _file);
            Render();
        }

        /// <summary>
        ///     This function is used to switch filters buttons state.
        /// </summary>
        public void SwitchFiltersState()
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

        /// <summary>
        ///     This function is called before appliying any filter.
        /// </summary>
        private void FiltersStart()
        {
            _filtersState = true;
            LoadButton.Enabled = false;
            RenderButton.Enabled = false;
            SwitchFiltersState();
        }

        /// <summary>
        ///     This function is called after a filter has been applied.
        /// </summary>
        private void FiltersEnd()
        {
            SwitchFiltersState();
            LoadButton.Enabled = true;
            if (_file != null)
                RenderButton.Enabled = true;
        }

        /*
        ** FILTERS START
        ** These functions are called to apply filters on rendered .obj files.
        */
        private void FiltersContrastMore_Click(object sender, EventArgs e)
        {
            FiltersStart();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new ContrastMore(new MyImage(_picture), new MyImage(result));
            RenderPicture.Image = result;
            FiltersEnd();
            FiltersContrastMore.Enabled = false;
        }

        private void FiltersBorderEnhancement_Click(object sender, EventArgs e)
        {
            FiltersStart();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new BorderMore(new MyImage(_picture), new MyImage(result));
            RenderPicture.Image = result;
            FiltersEnd();
            FiltersBorderEnhancement.Enabled = false;
        }

        private void FiltersBlur_Click(object sender, EventArgs e)
        {
            FiltersStart();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new Blur(new MyImage(_picture), new MyImage(result));
            RenderPicture.Image = result;
            FiltersEnd();
            FiltersBlur.Enabled = false;
        }

        private void FiltersBorderDetect_Click(object sender, EventArgs e)
        {
            FiltersStart();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new BorderDetect(new MyImage(_picture), new MyImage(result));
            RenderPicture.Image = result;
            FiltersEnd();
            FiltersBorderDetect.Enabled = false;

       }

        private void FiltersBorderDetectMore_Click(object sender, EventArgs e)
        {
            FiltersStart();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new BorderDetectMore(new MyImage(_picture), new MyImage(result));
            RenderPicture.Image = result;
            FiltersEnd();
            FiltersBorderDetectMore.Enabled = false;
        }

        private void FiltersPush_Click(object sender, EventArgs e)
        {
            FiltersStart();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new Push(new MyImage(_picture), new MyImage(result));
            RenderPicture.Image = result;
            FiltersEnd();
            FiltersPush.Enabled = false;
        }

        private void FiltersSharpeness_Click(object sender, EventArgs e)
        {
            FiltersStart();
            Bitmap result = new Bitmap(RenderPicture.Image);
            var filter = new Sharpen(new MyImage(_picture), new MyImage(result));
            RenderPicture.Image = result;
            FiltersEnd();
            FiltersSharpeness.Enabled = false;
        }

        /*
        ** FILTERS END.
        */
    }
}