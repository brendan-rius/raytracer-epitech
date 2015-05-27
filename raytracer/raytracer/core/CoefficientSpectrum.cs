using System;

namespace raytracer.core
{
    /// <summary>
    ///     Represent a coefficiented spectrum.
    /// </summary>
    public class CoefficientSpectrum
    {
        /// <summary>
        ///     The number of samples
        /// </summary>
        protected ushort _nsamples;

        /// <summary>
        ///     Samples values
        /// </summary>
        protected float[] _samples;

        /// <summary>
        ///     Create a coefficient spectrum based on a number of samples
        ///     and an optional default value for these samples
        /// </summary>
        /// <param name="nsamples">the number of samples</param>
        /// <param name="defaultValue">the defalt value (0 by default)</param>
        protected CoefficientSpectrum(ushort nsamples, float defaultValue = 0)
        {
            _samples = new float[nsamples];
            _nsamples = nsamples;
            for (var i = 0; i < nsamples; ++i)
                _samples[i] = defaultValue;
        }

        public ushort NSamples
        {
            get { return _nsamples; }
        }

        public float[] Samples
        {
            get { return _samples; }
        }

        /// <summary>
        ///     Add two spectrums together.
        /// </summary>
        /// <param name="s1">the left spectrum</param>
        /// <param name="s2">the right spectrum</param>
        /// <returns>the new spectrum</returns>
        public static CoefficientSpectrum operator +(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            if (s1._nsamples != s2._nsamples)
                throw new Exception("Spectrums do not have the same number of samples");
            var resultSpectrum = new CoefficientSpectrum(s1._nsamples);
            for (var i = 0; i < s1._nsamples; ++i)
                resultSpectrum._samples[i] = s1._samples[i] + s2._samples[i];
            return resultSpectrum;
        }

        /// <summary>
        ///     Subtracts two spectrums together.
        /// </summary>
        /// <param name="s1">the left spectrum</param>
        /// <param name="s2">the right spectrum</param>
        /// <returns>the new spectrum</returns>
        public static CoefficientSpectrum operator -(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            if (s1._nsamples != s2._nsamples)
                throw new Exception("Spectrums do not have the same number of samples");
            var resultSpectrum = new CoefficientSpectrum(s1._nsamples);
            for (var i = 0; i < s1._nsamples; ++i)
                resultSpectrum._samples[i] = s1._samples[i] - s2._samples[i];
            return resultSpectrum;
        }

        /// <summary>
        ///     Multiply two spectrums together.
        /// </summary>
        /// <param name="s1">the left spectrum</param>
        /// <param name="s2">the right spectrum</param>
        /// <returns>the new spectrum</returns>
        public static CoefficientSpectrum operator *(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            if (s1._nsamples != s2._nsamples)
                throw new Exception("Spectrums do not have the same number of samples");
            var resultSpectrum = new CoefficientSpectrum(s1._nsamples);
            for (var i = 0; i < s1._nsamples; ++i)
                resultSpectrum._samples[i] = s1._samples[i]*s2._samples[i];
            return resultSpectrum;
        }

        /// <summary>
        ///     Divide two spectrums together.
        ///     The new spectrum may have NaN values if a division by zero happens.
        /// </summary>
        /// <param name="s1">the left spectrum</param>
        /// <param name="s2">the right spectrum</param>
        /// <returns>the new spectrum</returns>
        public static CoefficientSpectrum operator /(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            if (s1._nsamples != s2._nsamples)
                throw new Exception("Spectrums do not have the same number of samples");
            var resultSpectrum = new CoefficientSpectrum(s1._nsamples);
            for (var i = 0; i < s1._nsamples; ++i)
                resultSpectrum._samples[i] = s1._samples[i]/s2._samples[i];
            return resultSpectrum;
        }

        /// <summary>
        ///     Multiply a spectrum and a value
        /// </summary>
        /// <param name="s1">the left spectrum</param>
        /// <param name="value"></param>
        /// <returns>the new spectrum</returns>
        public static CoefficientSpectrum operator *(CoefficientSpectrum s1, float value)
        {
            var resultSpectrum = new CoefficientSpectrum(s1._nsamples);
            for (var i = 0; i < s1._nsamples; ++i)
                resultSpectrum._samples[i] = s1._samples[i]*value;
            return resultSpectrum;
        }

        /// <summary>
        ///     Divide a spectrum by a value.
        ///     The spectrum may have NaN's value if a divisoi by zero happen.
        /// </summary>
        /// <param name="s1">the left spectrum</param>
        /// <param name="value">the value to divide by</param>
        /// <returns>the new spectrum</returns>
        public static CoefficientSpectrum operator /(CoefficientSpectrum s1, float value)
        {
            var resultSpectrum = new CoefficientSpectrum(s1._nsamples);
            for (var i = 0; i < s1._nsamples; ++i)
                resultSpectrum._samples[i] = s1._samples[i]/value;
            return resultSpectrum;
        }

        /// <summary>
        ///     Subtract a spectrum and a value
        /// </summary>
        /// <param name="s1">the left spectrum</param>
        /// <param name="value"></param>
        /// <returns>the new spectrum</returns>
        public static CoefficientSpectrum operator -(CoefficientSpectrum s1, float value)
        {
            var resultSpectrum = new CoefficientSpectrum(s1._nsamples);
            for (var i = 0; i < s1._nsamples; ++i)
                resultSpectrum._samples[i] = s1._samples[i] - value;
            return resultSpectrum;
        }

        /// <summary>
        ///     Add a spectrum and a value
        /// </summary>
        /// <param name="s1">the left spectrum</param>
        /// <param name="value"></param>
        /// <returns>the new spectrum</returns>
        public static CoefficientSpectrum operator +(CoefficientSpectrum s1, float value)
        {
            var resultSpectrum = new CoefficientSpectrum(s1._nsamples);
            for (var i = 0; i < s1._nsamples; ++i)
                resultSpectrum._samples[i] = s1._samples[i] + value;
            return resultSpectrum;
        }

        /// <summary>
        ///     Negate a spectrum
        /// </summary>
        /// <param name="s">the spectrum to negate</param>
        /// <returns>the negated spectrum</returns>
        public static CoefficientSpectrum operator -(CoefficientSpectrum s)
        {
            var resultSpectrum = new CoefficientSpectrum(s._nsamples);
            for (var i = 0; i < s._nsamples; ++i)
                resultSpectrum._samples[i] = -resultSpectrum._samples[i];
            return resultSpectrum;
        }

        /// <summary>
        ///     Perform linear interpolation on two spectrums
        /// </summary>
        /// <param name="s1">the first spectrum</param>
        /// <param name="s2">the second spectrum</param>
        /// <param name="t">the time value</param>
        /// <returns>the new spectrum</returns>
        public CoefficientSpectrum Lerp(CoefficientSpectrum s1, CoefficientSpectrum s2, float t)
        {
            return s1*(1f - t) + s2*t;
        }

        /// <summary>
        ///     In order to be equal two spectrum must have the same number of samples
        ///     and they must be equal.
        /// </summary>
        /// <param name="obj">the object to compare to</param>
        /// <returns>true if equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            var s = obj as CoefficientSpectrum;
            if (s == null)
                return false;
            if (s._nsamples != _nsamples)
                return false;
            for (var i = 0; i < s._nsamples; ++i)
            {
                if (s._samples[i] != _samples[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        ///     Clamp a spectrum
        /// </summary>
        /// <param name="s">the spectrum to clamp</param>
        /// <param name="low">the minimum value</param>
        /// <param name="high">the maximum value</param>
        /// <returns>the clamped spectrum</returns>
        public static CoefficientSpectrum Clamp(CoefficientSpectrum s, float low = 0,
            float high = float.PositiveInfinity)
        {
            var result = new CoefficientSpectrum(s._nsamples);
            for (var i = 0; i < s._nsamples; ++i)
                result._samples[i] = result._samples[i] < low ? low : (result._samples[i] > high ? high : low);
            return result;
        }

        /// <summary>
        ///     Check is the spectrum is black
        /// </summary>
        /// <returns>true if the spectrum is black, false otherwise</returns>
        public bool IsBlack()
        {
            for (var i = 0; i < _nsamples; ++i)
            {
                if (_samples[i] != 0f)
                    return false;
            }
            return true;
        }

        /// <summary>
        ///     Check is the spectrum contains NaN values.
        ///     This may be the case when divising a sample by zero
        /// </summary>
        /// <returns>true is the spectrum has NaN, false otherwise</returns>
        public bool HasNaNs()
        {
            for (var i = 0; i < _nsamples; ++i)
                if (float.IsNaN(_samples[i]))
                    return true;
            return false;
        }
    }
}