﻿using System.Collections.Generic;
using System.Linq;

namespace raytracer.core
{
    /// <summary>
    ///     A primitive is an object in the scene.
    ///     It is made of a geometric shape (used to test intersections), and
    ///     a material (used for shading)
    /// </summary>
    public class Primitive : IIntersectable
    {
        /// <summary>
        ///     Create a primitive from a shape and a material
        /// </summary>
        /// <param name="shape">the shape</param>
        /// <param name="material">the material</param>
        public Primitive(Shape shape, Material material)
        {
            Shape = shape;
            Material = material;
        }

        /// <summary>
        ///     The shape of the primitive
        /// </summary>
        public Shape Shape { get; private set; }

        /// <summary>
        ///     The material of the primitive
        /// </summary>
        public Material Material { get; private set; }

        public bool TryToIntersect(Ray ray, ref Intersection intersection)
        {
            if (!Shape.TryToIntersect(ray, ref intersection)) return false;
            intersection.Primitive = this;
            return true;
        }

        public bool Intersect(Ray ray)
        {
            return Shape.Intersect(ray);
        }

        /// <summary>
        ///     Get the BSDF of the primitive at an intersection
        /// </summary>
        /// <param name="intersection">the intersection</param>
        /// <returns>the BSDF</returns>
        public BSDF GetBSDF(ref Intersection intersection)
        {
            return Material.GetBSDF(ref intersection);
        }

        /// <summary>
        ///     Returns Whether a ray can intersect with the primitive.
        /// </summary>
        /// <returns></returns>
        public bool CanIntersect()
        {
            return Shape.CanIntersect();
        }

        /// <summary>
        ///     Refine the primitive.
        /// </summary>
        /// <param name="primitives"></param>
        public void Refine(List<Primitive> primitives)
        {
            var shapes = new List<Shape>();

            Shape.Refine(shapes);
            primitives.AddRange(shapes.Select(s => new Primitive(s, Material)).ToList());
        }

        public BBox WorldBound()
        {
            return Shape.WorldBound();
        }
    }
}