using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raytracer.filters
{
    /// <summary>
    ///     This class creates a new filter on an image.
    ///     In this case a more important contrast filter.
    /// </summary>
    public class ContrastMore : Filter
    {
        public ContrastMore(IImage map, IImage new_map)
        {
            Int32[,] matrix;

            matrix = new Int32[3, 3] {
                { 0, -1,  0},
                {-1,  5, -1},
                { 0, -1,  0}
            };
            NewFilter(matrix, map, new_map);
        }
    }

    /// <summary>
    ///     This class creates a new filter on an image.
    ///     In this case border are more visible.
    /// </summary>
    public class BorderMore : Filter
    {
        public BorderMore(IImage map, IImage new_map)
        {
            Int32[,] matrix;

            matrix = new Int32[3, 3] {
                { 0,  0,  0},
                {-1,  1,  0},
                { 0,  0,  0}
            };
            NewFilter(matrix, map, new_map);
        }
    }

    /// <summary>
    ///     This class creates a new filter on an image.
    ///     In this case image will be blurred.
    /// </summary>
    public class Blur: Filter
    {
        public Blur(IImage map, IImage new_map)
        {
            Int32[,] matrix;

            matrix = new Int32[3, 3] {
                { 1,  1,  1},
                { 1,  1,  1},
                { 1,  1,  1}
            };
            NewFilter(matrix, map, new_map);
        }
    }

    /// <summary>
    ///     This class creates a new filter on an image.
    ///     In this case we are able to detect borders.
    /// </summary>
    public class BorderDetect : Filter
    {
        public BorderDetect(IImage map, IImage new_map)
        {
            Int32[,] matrix;

            matrix = new Int32[3, 3] {
                { 0,  1,  0},
                { 1, -4,  1},
                { 0,  1,  0}
            };
            NewFilter(matrix, map, new_map);
        }
    }

    /// <summary>
    ///     This class creates a new filter on an image.
    ///     In this case a 'push' effect.
    /// </summary>
    public class Push : Filter
    {
        public Push(IImage map, IImage new_map)
        {
            Int32[,] matrix;

            matrix = new Int32[3, 3] {
                {-2, -1,  0},
                {-1,  1,  1},
                { 0,  1,  2}
            };
            NewFilter(matrix, map, new_map);
        }
    }

    /// <summary>
    ///     This class creates a new filter on an image.
    ///     In this case we are able to better detect borders.
    /// </summary>
    public class BorderDetectMore : Filter
    {
        public BorderDetectMore(IImage map, IImage new_map)
        {
            Int32[,] matrix;

            matrix = new Int32[3, 3] {
                { 1,  1,  1},
                { 1, -8,  1},
                { 1,  1,  1}
            };
            NewFilter(matrix, map, new_map);
        }
    }

    /// <summary>
    ///     This class creates a new filter on an image.
    ///     In this case we are able to sharpen an image.
    /// </summary>
    public class Sharpen : Filter
    {
        public Sharpen(IImage map, IImage new_map)
        {
            Int32[,] matrix;

            matrix = new Int32[3, 3] {
                {-1, -1, -1},
                {-1,  9, -1},
                {-1, -1, -1}
            };
            NewFilter(matrix, map, new_map);
        }
    }

    public abstract class Filter
    {
        /// <summary>
        ///     This function iterates a map and apply the requested filter into a new map.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public void NewFilter(Int32[,] transf, IImage map, IImage new_map)
        {
            Int32[,]    matrix = new Int32[3, 3];

            for (uint y = 0; y < map.YLimit; y++)
            {
                for (uint x = 0; x < map.XLimit; x++)
                {
                    ResetMatrix(matrix);
                    FillMatrix(x, y, matrix, map);
                    new_map.PutPixel(x, y, CalcFilter(transf, matrix));
                }
            }
            return ;
        }

        /// <summary>
        ///     This function resets a filter 3x3 matrix.
        /// </summary>
        /// <param name="matrix">The matrix to reset.</param>
        /// <returns></returns>
        private void    ResetMatrix(Int32[,] matrix)
        {
            for (uint y = 0; y < 3; y++)
            {
                for (uint x = 0; x < 3; x++)
                {
                    matrix[y, x] = 0;
                }
            }
            return;
        }

        /// <summary>
        ///     This method is used to fill the matrix array with points of the original map.
        /// </summary>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        /// <param name="matrix">Matrix where to add data.</param>
        /// <param name="map">The map interface.</param>
        private void    FillMatrix(uint x, uint y, Int32[,] matrix, IImage map)
        {
            Int32 pixel;
            if (y > 0)
            {
                matrix[0, 0] = (x > 0 ? map.GetPixel(x - 1, y - 1) : 0);
                matrix[0, 1] = map.GetPixel(x, y - 1);
                matrix[0, 2] = (x < map.XLimit - 1 ? map.GetPixel(x + 1, y - 1) : 0);
            }
            matrix[1, 0] = (x > 0 ? map.GetPixel(x - 1, y) : 0);
            matrix[1, 1] = map.GetPixel(x, y);
            matrix[1, 2] = (x < map.XLimit - 1 ? map.GetPixel(x + 1, y) : 0);
            if (y < map.YLimit - 1)
            {
                matrix[2, 0] = (x > 0 ? map.GetPixel(x - 1, y + 1) : 0);
                matrix[2, 1] = map.GetPixel(x, y + 1);
                matrix[2, 2] = (x < map.XLimit - 1 ? map.GetPixel(x + 1, y + 1) : 0);
            }
            return;
        }

        /// <summary>
        ///     This function is used to calculate the requested filter.
        /// </summary>
        /// <param name="transformation">The transformation matrix.</param>
        /// <param name="matrix">The pixel's neighbour matrix to calculate</param>
        /// <returns></returns>
        private Int32   CalcFilter(Int32[,] transf, Int32[,] matrix)
        {
            Int32 result = 0;
            float[,] R = new float[3, 3];
            float[,] G = new float[3, 3];
            float[,] B = new float[3, 3];

            for (uint y = 0; y < 3; y++)
            {
                for (uint x = 0; x < 3; x++)
                {
                    R[y, x] = ((matrix[y, x] >> 16) & 0xFF) / 255.0F;
                    G[y, x] = ((matrix[y, x] >> 8) & 0xFF) / 255.0F;
                    B[y, x] = (matrix[y, x] & 0xFF) / 255.0F;
                }
            }
            result |= 255 << 24;
            result |= CalcRes(transf, R) << 16;
            result |= CalcRes(transf, G) << 8;
            result |= CalcRes(transf, B);
            return (result);
        }

        /// <summary>
        ///     This function is used to calculates the result for each color channel.
        /// </summary>
        /// <param name="transf">points to calculate</param>
        /// <param name="color">color channel</param>
        /// <returns></returns>
        private Int32 CalcRes(Int32[,] transf, float[,] color)
        {
            float result;

            result =
                transf[0, 0] * color[0, 0] + transf[0, 1] * color[0, 1] + transf[0, 2] * color[0, 2] +
                transf[1, 0] * color[1, 0] + transf[1, 1] * color[1, 1] + transf[1, 2] * color[2, 2] +
                transf[2, 0] * color[2, 0] + transf[2, 1] * color[2, 1] + transf[2, 2] * color[2, 2];
            if (result >= 0.0F && result <= 1.0F)
                return ((Int32) (result * 255.0F));
            return (result < 0.0F ? 0 : 255);
        }
    }

    /// <summary>
    ///     This interface is used to edit the scene to add filters.
    /// </summary>
    public interface IImage
    {
        Int32 GetPixel(uint x, uint y);

        void PutPixel(uint x, uint y, Int32 color);

        uint XLimit { get; set; }
        uint YLimit { get; set; }
    }
}