using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using OpenTK;
using rt;
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
using System.Text.RegularExpressions;
using System.Threading;

namespace RT_2_poule
{
    public partial class Form1 : Form
    {
        private string _file;
        private const uint NSamples = 1;
        private Renderer _renderer;
        private Scene _scene;
        private Camera _camera;
        private Screen _screen;
        private MyFilm _film;
        private Regex _pointLight;
        private Regex _diskLight;
        private Regex _obj;
        private Regex _cameraReg;

        public Form1()
        {
            InitializeComponent();
            FileName.ForeColor = System.Drawing.Color.FromArgb((int)0xFF, (int)0x61, (int)0x61);
            FileName.Text = "No file selected.";
            _screen = new Screen((uint)RenderPicture.Width, (uint)RenderPicture.Height);
            _camera = new SimpleCamera(_screen,
                Transformation.Compose(
                    Transformation.Translation(0, 0, -1000),
                    Transformation.RotateX(0),
                    Transformation.RotateY(0),
                    Transformation.RotateZ(0)
                ));
            _pointLightReg = new Regex(@"/^(PL( (-?|\+?)(\d)* (-?|\+?)(\d)* (-?|\+?)(\d)* (1|0)\.(\d)*$))/gm",
                RegexOptions.Compiled);
            _diskLightReg = new Regex(@"/^(DL( (-?|\+?)(\d)* (-?|\+?)(\d)* (-?|\+?)(\d)* (1|0)\.(\d)* (\d*\.)?(\d)*$))/gm",
                RegexOptions.Compiled);
            _objReg = new Regex(@"/^(O( [A-Z]:(\\([a-zA-Z]*[^*/\\.\[\]:;|=,])*)*\.obj$))/gm",
                RegexOptions.Compiled);
            _cameraReg = new Regex(@"/^(C( (-?|\+?)(\d)* (-?|\+?)(\d)* (-?|\+?)(\d)* (-?|\+?)(\d)* (-?|\+?)(\d)* (-?|\+?)(\d)*$))/gm",
                RegexOptions.Compiled);
        }

        /// <summary>
        ///     This function is called to create a new scene to render.
        /// </summary>
        private void InitNewScene()
        {
            _film = new MyFilm(_screen, NSamples);
            _scene = new Scene();
            _renderer = new Renderer(_scene, new GridSampler(_screen), _camera, _film, new WhittedIntegrator());
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

        /// <summary>
        ///     This function is used to render a scene.
        /// </summary>
        private async void Render()
        {
            await Task.Run(() => _renderer.Render());
            _film.Display(RenderPicture);
            RenderPicture.Image = new Bitmap(RenderPicture.Image);
            FileName.ForeColor = System.Drawing.Color.FromArgb((int)0xFF, (int)0x61, (int)0x61);
            FileName.Text = "No file selected.";
            _file = null;
            LoadFile.Enabled = true;
            SaveScene.Enabled = true;
        }

        /// <summary>
        ///     This function is used to parse a .poule file.
        /// </summary>
        private void ParsePoule(Scene scene, string filename)
        {
            var lines = File.ReadAllLines(filename);
            foreach (string str in lines)
            {
                Match PointLightMatch = _pointLight.Match(str);
                Match DiskLightMatch = _diskLight.Match(str);
                Match CameraMatch = _cameraReg.Match(str);
                Match ObjMatch = _obj.Match(str);
                if (PointLightMatch.Success == true)
                {
                    string[] array = str.Split(' ');
                    scene.Lights.Add(new PointLight(Transformation.Translation(
                        float.Parse(array[1], CultureInfo.InvariantCulture.NumberFormat),
                        float.Parse(array[2], CultureInfo.InvariantCulture.NumberFormat),
                        float.Parse(array[3], CultureInfo.InvariantCulture.NumberFormat))));
                    // luminance ???
                }
                else if (DiskLightMatch.Success == true)
                {
                    string[] array = str.Split(' ');
                    // -> DISK LIGHT
                    scene.Lights.Add(new PointLight(Transformation.Translation(
                        float.Parse(array[1], CultureInfo.InvariantCulture.NumberFormat),
                        float.Parse(array[2], CultureInfo.InvariantCulture.NumberFormat),
                        float.Parse(array[3], CultureInfo.InvariantCulture.NumberFormat))));
                    // luminance ???
                }
                else if (CameraMatch.Success == true)
                {
                    string[] array = str.Split(' ');
                    _camera = new SimpleCamera(_screen,
                        Transformation.Compose(
                            Transformation.Translation(
                                Convert.ToInt32(array[1]),
                                Convert.ToInt32(array[2]),
                                Convert.ToInt32(array[3])
                            ),
                            Transformation.RotateX(float.Parse(array[4], CultureInfo.InvariantCulture.NumberFormat)),
                            Transformation.RotateY(float.Parse(array[5], CultureInfo.InvariantCulture.NumberFormat)),
                            Transformation.RotateZ(float.Parse(array[6], CultureInfo.InvariantCulture.NumberFormat))
                        ));
                }
                else if (ObjMatch.Success == true)
                {
                    string[] array = str.Split(' ');
                    try
                    {
                        ParsingObj parser = new ParsingObj(_scene, array[1]);
                        scene.Elements.Add(parser);
                    }
                    catch (Exception e)
                    {
                        FileName.Text = e.Message;
                    }
                }
            }
        }

        /// <summary>
        ///     This function is used when the 'exit' button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
                System.Windows.Forms.Application.Exit();
            else
                System.Environment.Exit(1);
        }

        /// <summary>
        ///     This function is called when the 'load' button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFile_Click(object sender, EventArgs e)
        {
            DialogResult File = OpenFile.ShowDialog();
            if (File == DialogResult.OK)
            {
                _file = OpenFile.FileName;
                if (!_file.EndsWith(".poule"))
                {
                    RenderScene.Enabled = false;
                    FileName.ForeColor = System.Drawing.Color.FromArgb((int)0xFF, (int)0x61, (int)0x61);
                    FileName.Text = "Please select a valid filename (.poule).";
                    return;
                }
                FileName.ForeColor = System.Drawing.Color.FromArgb((int)0x40, (int)0x40, (int)0x40);
                FileName.Text = _file;
                ParsePoule(_scene, _file);
                RenderScene.Enabled = true;
            }
        }

        /// <summary>
        ///     This function is called when the 'render' button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenderScene_Click(object sender, EventArgs e)
        {
            var rt = new RayTracer();
            InitNewScene();
            RenderPicture.Image = null;
            LoadFile.Enabled = false;
            RenderScene.Enabled = false;
            SaveScene.Enabled = false;
            ParsePoule(_scene, _file);
            Render();
        }

        /// <summary>
        ///     This funcion is called when the 'save' button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveScene_Click(object sender, EventArgs e)
        {
            DialogResult File = SaveFile.ShowDialog();
            if (File == DialogResult.OK)
            {
                string file = SaveFile.FileName;
                RenderPicture.Image.Save(file, ImageFormat.Png);
            }
        }

        public Regex _diskLightReg { get; set; }

        public Regex _pointLightReg { get; set; }

        public Regex _objReg { get; set; }
    }
}
