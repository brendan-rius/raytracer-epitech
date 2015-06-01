using System;
using System.Linq;
using OpenTK;

namespace raytracer.core.mathematics
{
    /// <summary>
    ///     A transformation represents a geometric transformation and is used to transform
    ///     rays
    /// </summary>
    public class Transformation
    {
        /// <summary>
        ///     A transformation that does nothing
        /// </summary>
        public static readonly Transformation Identity = new Transformation(Matrix4.Identity);

        /* 
         * Both the inverse transformation and the inverse matrix are lazy loaded
         * since a Matrix is a struct (and this it takes a lot of memory) and because
         * inverting a matrix is a costly operation
         */
        private readonly Lazy<Matrix4> _inverseTransformationMatrixLazy;

        /// <summary>
        ///     Get the matrix of the transformation
        /// </summary>
        public Matrix4 TransformationMatrix;

        /// <summary>
        ///     Create a transformation from a matrix
        /// </summary>
        /// <param name="transformationMatrix">the matrix for the transformation</param>
        public Transformation(Matrix4 transformationMatrix)
        {
            TransformationMatrix = transformationMatrix;
            _inverseTransformationMatrixLazy = new Lazy<Matrix4>(() =>
            {
                Matrix4 m;
                Matrix4.Invert(ref transformationMatrix, out m);
                return m;
            });
        }

        /// <summary>
        ///     Create a matrix from a transformation and its inverse. This is less costly.
        /// </summary>
        /// <param name="transformationMatrix">the matrix</param>
        /// <param name="inverseTransformationMatrix">the inverse of the matrix</param>
        public Transformation(Matrix4 transformationMatrix, Matrix4 inverseTransformationMatrix)
        {
            TransformationMatrix = transformationMatrix;
            /* Here the lazy loading is kind of useless since it returns a already known value, but
             * using declaring another field in the class would have make us use more memory */
            _inverseTransformationMatrixLazy = new Lazy<Matrix4>(() => inverseTransformationMatrix);
        }

        /// <summary>
        ///     Get the inverse matrix of the transformation.
        ///     <remarks>
        ///         You can safely call this method multiple time, since the inverse
        ///         will be cached.
        ///     </remarks>
        /// </summary>
        public Matrix4 InverseTransformationMatrix
        {
            get { return _inverseTransformationMatrixLazy.Value; }
        }

        /// <summary>
        ///     Compute the inverse transformation of this transformation.
        ///     <remarks>
        ///         You can safely call this getter multiple time, the value is cached
        ///     </remarks>
        /// </summary>
        public Transformation InverseTransformation
        {
            get { return new Transformation(InverseTransformationMatrix, TransformationMatrix); }
        }

        /// <summary>
        ///     Transform a ray
        /// </summary>
        /// <param name="ray">the ray to transform</param>
        /// <returns>a transformed ray</returns>
        public Ray TransformRay(Ray ray)
        {
            Vector3 direction;
            Vector3 origin;

            Vector3.TransformVector(ref ray.Direction, ref TransformationMatrix, out direction);
            Vector3.TransformPosition(ref ray.Origin, ref TransformationMatrix, out origin);
            return new Ray(direction, origin, ray.Start, ray.End, ray.Depth);
        }

        /// <summary>
        ///     A translation by a vector
        /// </summary>
        /// <param name="vector">the vector representing the trasnlation</param>
        /// <returns></returns>
        public static Transformation Translation(Vector3 vector)
        {
            return new Transformation(Matrix4.CreateTranslation(vector));
        }

        /// <summary>
        ///     A translation by a vector
        /// </summary>
        /// <param name="x">x coordinate of the vector</param>
        /// <param name="y">y coordinate of the vector</param>
        /// <param name="z">z coordinate of the vector</param>
        /// <returns>the transformation</returns>
        public static Transformation Translation(float x, float y, float z)
        {
            return new Transformation(Matrix4.CreateTranslation(x, y, z));
        }

        /// <summary>
        ///     Transform a point
        /// </summary>
        /// <param name="point">the point to transform</param>
        /// <param name="transformedPoint">the result point</param>
        public void TransformPoint(ref Vector3 point, out Vector3 transformedPoint)
        {
            Vector3.TransformPosition(ref point, ref TransformationMatrix, out transformedPoint);
        }

        /// <summary>
        ///     Transform a point
        /// </summary>
        /// <param name="point">the point to transform</param>
        /// <returns>the transformed point</returns>
        public Vector3 TransformPoint(ref Vector3 point)
        {
            Vector3 transformedPoint;
            Vector3.TransformPosition(ref point, ref TransformationMatrix, out transformedPoint);
            return transformedPoint;
        }

        /// <summary>
        ///     Transform a vector
        /// </summary>
        /// <param name="vector">the vector to transform</param>
        /// <param name="transformedVector">the result vector</param>
        public void TransformVector(ref Vector3 vector, out Vector3 transformedVector)
        {
            Vector3.TransformVector(ref vector, ref TransformationMatrix, out transformedVector);
        }

        /// <summary>
        ///     Transform a vector
        /// </summary>
        /// <param name="vector">the vector to transform</param>
        public Vector3 TransformVector(ref Vector3 vector)
        {
            Vector3 transformedVector;
            Vector3.TransformVector(ref vector, ref TransformationMatrix, out transformedVector);
            return transformedVector;
        }

        /// <summary>
        ///     Merge multiple transformations into a single one
        /// </summary>
        /// <param name="transformations">the list of transformations to merge</param>
        /// <returns>a single transformation made of all the passed transformations</returns>
        public static Transformation Compose(params Transformation[] transformations)
        {
            return transformations.Aggregate(Identity, (current, transformation) => current*transformation);
        }

        /// <summary>
        ///     Compose transformations
        /// </summary>
        /// <param name="left">the left transformation</param>
        /// <param name="right">the right transformation</param>
        /// <returns>the composed transformation</returns>
        public static Transformation operator *(Transformation left, Transformation right)
        {
            return new Transformation(right.TransformationMatrix*left.TransformationMatrix,
                right.InverseTransformationMatrix*left.InverseTransformationMatrix);
        }

        /// <summary>
        ///     Rotation around X axis
        /// </summary>
        /// <param name="x">the rotation angle in degrees</param>
        /// <returns>the transformation</returns>
        public static Transformation RotateX(float x)
        {
            return new Transformation(Matrix4.CreateRotationX(x));
        }

        /// <summary>
        ///     Rotation around Y axis
        /// </summary>
        /// <param name="y">the rotation angle in degrees</param>
        /// <returns>the transformation</returns>
        public static Transformation RotateY(float y)
        {
            return new Transformation(Matrix4.CreateRotationY(y));
        }

        /// <summary>
        ///     Rotation around Z axis
        /// </summary>
        /// <param name="z">the rotation angle in degrees</param>
        /// <returns>the transformation</returns>
        public static Transformation RotateZ(float z)
        {
            return new Transformation(Matrix4.CreateRotationZ(z));
        }

        /// <summary>
        ///     Scale anisotropically
        /// </summary>
        /// <param name="x">the x scaling factor</param>
        /// <param name="y">the y scaling factor</param>
        /// <param name="z">the z scaling factor</param>
        /// <returns>the transformation</returns>
        public static Transformation ScaleXYZ(float x, float y, float z)
        {
            return new Transformation(Matrix4.CreateScale(x, y, z));
        }

        /// <summary>
        ///     Scale uniformly by a factor
        /// </summary>
        /// <param name="factor">the scaling factor</param>
        /// <returns>the transformation</returns>
        public static Transformation Scale(float factor)
        {
            return new Transformation(Matrix4.CreateScale(factor, factor, factor));
        }
    }
}