using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using raytracer.core;

namespace librtTests
{
    [TestClass]
    public class TestSampledSpectrum
    {
        [TestMethod]
        public void TestRegularilySpacedConstantSPD()
        {
            const int space = (SampledSpectrum.WavelengthEnd - SampledSpectrum.WavelengthStart)/SampledSpectrum.SPDSamples;
            var samples = new SortedDictionary<float, float>();
            for (var i = SampledSpectrum.WavelengthStart;
                i < SampledSpectrum.WavelengthEnd;
                i += space)
                samples.Add(i, 400);
            var spectrum = SampledSpectrum.FromSamples(samples);
            foreach (var val in spectrum.Samples)
                Assert.AreEqual(val, 400);
        }

        [TestMethod]
        public void TestIrregularilySpacedConstantSPD()
        {
            const int space = (SampledSpectrum.WavelengthEnd - SampledSpectrum.WavelengthStart) / SampledSpectrum.SPDSamples;
            var rng = new Random();
            var samples = new SortedDictionary<float, float>();
            for (int i = SampledSpectrum.WavelengthStart; i < SampledSpectrum.WavelengthEnd; i += rng.Next(50))
                samples.Add(i, 400);
            var spectrum = SampledSpectrum.FromSamples(samples);
            foreach (var val in spectrum.Samples)
                Assert.AreEqual(val, 400);
        }

        [TestMethod]
        public void TestRegularilySpacedSPD()
        {
            const int space = (SampledSpectrum.WavelengthEnd - SampledSpectrum.WavelengthStart) / SampledSpectrum.SPDSamples;
            var samples = new SortedDictionary<float, float>();
            for (var i = SampledSpectrum.WavelengthStart;
                i < SampledSpectrum.WavelengthEnd;
                i += space)
                samples.Add(i, i);
            var spectrum = SampledSpectrum.FromSamples(samples);
            for (var i = 0; i + 1 < spectrum.NSamples; ++i)
            {
                Console.WriteLine(spectrum.Samples[i]);
                Assert.AreEqual(SampledSpectrum.WavelengthStart + i * space + space / 2, spectrum.Samples[i]);
            }
        }
    }
}