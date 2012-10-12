﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SphereVisual3D.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace HelixToolkit.Wpf
{
    using System.Windows;
    using System.Windows.Media.Media3D;

    /// <summary>
    /// A visual element that shows a sphere defined by center and radius.
    /// </summary>
    public class SphereVisual3D : MeshElement3D
    {
        /// <summary>
        /// The center property.
        /// </summary>
        public static readonly DependencyProperty CenterProperty = DependencyProperty.Register(
            "Center",
            typeof(Point3D),
            typeof(SphereVisual3D),
            new PropertyMetadata(new Point3D(0, 0, 0), GeometryChanged));

        /// <summary>
        /// The phi div property.
        /// </summary>
        public static readonly DependencyProperty PhiDivProperty = DependencyProperty.Register(
            "PhiDiv", typeof(int), typeof(SphereVisual3D), new PropertyMetadata(30, GeometryChanged));

        /// <summary>
        /// The radius property.
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius", typeof(double), typeof(SphereVisual3D), new PropertyMetadata(1.0, GeometryChanged));

        /// <summary>
        /// The theta div property.
        /// </summary>
        public static readonly DependencyProperty ThetaDivProperty = DependencyProperty.Register(
            "ThetaDiv", typeof(int), typeof(SphereVisual3D), new PropertyMetadata(60, GeometryChanged));

        /// <summary>
        /// Gets or sets the center of the sphere.
        /// </summary>
        /// <value>The center.</value>
        public Point3D Center
        {
            get
            {
                return (Point3D)this.GetValue(CenterProperty);
            }

            set
            {
                this.SetValue(CenterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the number of divisions in the phi direction (from "top" to "bottom").
        /// </summary>
        /// <value>The phi div.</value>
        public int PhiDiv
        {
            get
            {
                return (int)this.GetValue(PhiDivProperty);
            }

            set
            {
                this.SetValue(PhiDivProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the radius of the sphere.
        /// </summary>
        /// <value>The radius.</value>
        public double Radius
        {
            get
            {
                return (double)this.GetValue(RadiusProperty);
            }

            set
            {
                this.SetValue(RadiusProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the number of divisions in the theta direction (around the sphere).
        /// </summary>
        /// <value>The theta div.</value>
        public int ThetaDiv
        {
            get
            {
                return (int)this.GetValue(ThetaDivProperty);
            }

            set
            {
                this.SetValue(ThetaDivProperty, value);
            }
        }

        /// <summary>
        /// Do the tesselation and return the <see cref="MeshGeometry3D"/>.
        /// </summary>
        /// <returns>A triangular mesh geometry.</returns>
        protected override MeshGeometry3D Tessellate()
        {
            var builder = new MeshBuilder(true, true);
            builder.AddSphere(this.Center, this.Radius, this.ThetaDiv, this.PhiDiv);
            return builder.ToMesh();
        }

    }
}