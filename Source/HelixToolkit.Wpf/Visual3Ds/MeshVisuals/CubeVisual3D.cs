﻿using System.Windows.Media.Media3D;
using System.Windows;
using System.Numerics;

namespace HelixToolkit.Wpf;

/// <summary>
/// A visual element that displays a cube.
/// </summary>
public class CubeVisual3D : MeshElement3D
{
    /// <summary>
    /// Identifies the <see cref="Center"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty CenterProperty = DependencyProperty.Register(
        "Center", typeof(Point3D), typeof(CubeVisual3D), new UIPropertyMetadata(new Point3D(), GeometryChanged));

    /// <summary>
    /// Identifies the <see cref="SideLength"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty SideLengthProperty = DependencyProperty.Register(
        "SideLength", typeof(double), typeof(CubeVisual3D), new UIPropertyMetadata(1.0, GeometryChanged));

    /// <summary>
    /// Gets or sets the center of the cube.
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
    /// Gets or sets the length of the cube sides.
    /// </summary>
    /// <value>The length of the sides.</value>
    public double SideLength
    {
        get
        {
            return (double)this.GetValue(SideLengthProperty);
        }

        set
        {
            this.SetValue(SideLengthProperty, value);
        }
    }

    /// <summary>
    /// Do the tessellation and return the <see cref="MeshGeometry3D"/>.
    /// </summary>
    /// <returns>The mesh geometry.</returns>
    protected override MeshGeometry3D? Tessellate()
    {
        var b = new MeshBuilder(false, true);
        b.AddCubeFace(
            this.Center.ToVector3(),
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, 1),
            (float)this.SideLength,
            (float)this.SideLength,
            (float)this.SideLength);
        b.AddCubeFace(
            this.Center.ToVector3(),
            new Vector3(1, 0, 0),
            new Vector3(0, 0, -1),
            (float)this.SideLength,
            (float)this.SideLength,
            (float)this.SideLength);
        b.AddCubeFace(
            this.Center.ToVector3(),
            new Vector3(0, -1, 0),
            new Vector3(0, 0, 1),
            (float)this.SideLength,
            (float)this.SideLength,
            (float)this.SideLength);
        b.AddCubeFace(
            this.Center.ToVector3(),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, -1),
            (float)this.SideLength,
            (float)this.SideLength,
            (float)this.SideLength);
        b.AddCubeFace(
            this.Center.ToVector3(),
            new Vector3(0, 0, 1),
            new Vector3(0, -1, 0),
            (float)this.SideLength,
            (float)this.SideLength,
            (float)this.SideLength);
        b.AddCubeFace(
            this.Center.ToVector3(),
            new Vector3(0, 0, -1),
            new Vector3(0, 1, 0),
            (float)this.SideLength,
            (float)this.SideLength,
            (float)this.SideLength);

        return b.ToMesh().ToWndMeshGeometry3D();
    }
}
