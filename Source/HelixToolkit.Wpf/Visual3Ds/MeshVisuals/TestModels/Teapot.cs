﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Teapot.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: Ms-PL
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace HelixToolkit.Wpf
{
    using System;
    using System.Windows;
    using System.Windows.Media.Media3D;

    /// <summary>
    /// A visual element that shows the Utah teapot test model.
    /// </summary>
    public class Teapot : MeshElement3D
    {
        #region Constants and Fields

        /// <summary>
        ///   The position property.
        /// </summary>
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
            "Position", typeof(Point3D), typeof(Teapot), new UIPropertyMetadata(new Point3D(0, 0, 1), TransformChanged));

        /// <summary>
        ///   The spout direction property.
        /// </summary>
        public static readonly DependencyProperty SpoutDirectionProperty = DependencyProperty.Register(
            "SpoutDirection", 
            typeof(Vector3D), 
            typeof(Teapot), 
            new UIPropertyMetadata(new Vector3D(1, 0, 0), TransformChanged));

        /// <summary>
        ///   The up direction property.
        /// </summary>
        public static readonly DependencyProperty UpDirectionProperty = DependencyProperty.Register(
            "UpDirection", 
            typeof(Vector3D), 
            typeof(Teapot), 
            new UIPropertyMetadata(new Vector3D(0, 0, 1), TransformChanged));

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Point3D Position
        {
            get
            {
                return (Point3D)this.GetValue(PositionProperty);
            }

            set
            {
                this.SetValue(PositionProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets the spout direction.
        /// </summary>
        /// <value>The spout direction.</value>
        public Vector3D SpoutDirection
        {
            get
            {
                return (Vector3D)this.GetValue(SpoutDirectionProperty);
            }

            set
            {
                this.SetValue(SpoutDirectionProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets up direction.
        /// </summary>
        /// <value>Up direction.</value>
        public Vector3D UpDirection
        {
            get
            {
                return (Vector3D)this.GetValue(UpDirectionProperty);
            }

            set
            {
                this.SetValue(UpDirectionProperty, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Do the tesselation and return the <see cref="MeshGeometry3D"/>.
        /// </summary>
        /// <returns>
        /// </returns>
        protected override MeshGeometry3D Tessellate()
        {
            var rd =
                Application.LoadComponent(
                    new Uri("HelixToolkit.Wpf;component/Resources/TeapotGeometry.xaml", UriKind.Relative)) as
                ResourceDictionary;
            this.OnTransformChanged();
            return rd["TeapotGeometry"] as MeshGeometry3D;
        }

        /// <summary>
        /// The transform changed.
        /// </summary>
        /// <param name="d">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private static void TransformChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Teapot)d).OnTransformChanged();
        }

        /// <summary>
        /// Called when the transform is changed.
        /// </summary>
        private void OnTransformChanged()
        {
            Vector3D right = this.SpoutDirection;
            Vector3D back = Vector3D.CrossProduct(this.UpDirection, right);
            Vector3D up = this.UpDirection;

            this.Transform =
                new MatrixTransform3D(
                    new Matrix3D(
                        right.X, 
                        right.Y, 
                        right.Z, 
                        0, 
                        up.X, 
                        up.Y, 
                        up.Z, 
                        0, 
                        back.X, 
                        back.Y, 
                        back.Z, 
                        0, 
                        this.Position.X, 
                        this.Position.Y, 
                        this.Position.Z, 
                        1));
        }

        #endregion
    }
}