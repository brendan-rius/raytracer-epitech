using System;
using System.Diagnostics;
using raytracer.cameras;
using raytracer.core;
using raytracer.core.mathematics;
using raytracer.primitives;
using raytracer.samplers;

namespace console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var scene = new Scene();
            scene.Elements.Add(new Sphere());
            var screen = new Screen(1920, 1080);
            var film = new MyFilm(screen);
            var camera = new SimpleCamera(screen, Transformation.Translation(0, 0, 2));
            var renderer = new Renderer(scene, new GridSampler(screen), camera, film);
            var watch = new Stopwatch();
            watch.Start();
            renderer.Render();
            watch.Stop();
            Console.WriteLine("Rendering done in " + watch.ElapsedMilliseconds + " milliseconds");
        }
    }

    internal class MyFilm : Film
    {
        public MyFilm(Screen screen) : base(screen)
        {
        }

        public override void AddSample(Sample sample, RGBSpectrum spectrum)
        {
        }
    }
}