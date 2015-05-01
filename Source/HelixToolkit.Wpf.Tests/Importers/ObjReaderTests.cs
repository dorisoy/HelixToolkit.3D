﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjReaderTests.cs" company="Helix Toolkit">
//   Copyright (c) 2014 Helix Toolkit contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace HelixToolkit.Wpf.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Media.Media3D;

    using HelixToolkit.Wpf;
    using NUnit.Framework;

    // ReSharper disable InconsistentNaming
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class ObjReaderTests 
    {
        private ObjReader _objReader;

        [SetUp]
        public void SetUp() 
        {
            _objReader = new ObjReader();              
        }

        [Test, Ignore]
        public void Read_Bunny_ValidModel()
        {
            var model = _objReader.Read(@"Models\obj\bunny.obj");
            Assert.AreEqual(2, model.Children.Count);
            var gm1 = model.Children[1] as GeometryModel3D;
            Assert.NotNull(gm1);
            var mg1 = gm1.Geometry as MeshGeometry3D;
            Assert.NotNull(mg1);
            Assert.AreEqual(69451, mg1.TriangleIndices.Count / 3);
        }

        [Test]
        public void Read_CornellBox_ValidModel()
        {
            var model = _objReader.Read(@"Models\obj\cornell_box.obj");
            Assert.AreEqual(9, model.Children.Count);
            var gm1 = model.Children[1] as GeometryModel3D;
            Assert.NotNull(gm1);
            var mg1 = gm1.Geometry as MeshGeometry3D;
            Assert.NotNull(mg1);
            //// Assert.AreEqual(69451, mg1.TriangleIndices.Count / 3);
        }

        [Test, Ignore]
        public void Read_Ducky_ValidModel()
        {
            var model = _objReader.Read(@"Models\obj\ducky.obj");
            Assert.AreEqual(4, model.Children.Count);
            var m0 = (MeshGeometry3D)((GeometryModel3D)model.Children[0]).Geometry;
            var m1 = (MeshGeometry3D)((GeometryModel3D)model.Children[1]).Geometry;
            var m2 = (MeshGeometry3D)((GeometryModel3D)model.Children[2]).Geometry;
            var m3 = (MeshGeometry3D)((GeometryModel3D)model.Children[3]).Geometry;
            Assert.AreEqual(5632, m0.TriangleIndices.Count / 3);
            Assert.AreEqual(4800, m1.TriangleIndices.Count / 3);
            Assert.AreEqual(3024, m2.TriangleIndices.Count / 3);
            Assert.AreEqual(672, m3.TriangleIndices.Count / 3);
        }

        [Test]
        public void Read_Test_ValidModel()
        {
            var model = _objReader.Read(@"Models\obj\test.obj");
            Assert.AreEqual(2, model.Children.Count);
            var gm1 = model.Children[0] as GeometryModel3D;
            Assert.NotNull(gm1);
            var mg1 = gm1.Geometry as MeshGeometry3D;
            Assert.NotNull(mg1);
            Assert.AreEqual(12, mg1.TriangleIndices.Count / 3);
        }

        [Test]
        public void Read_SmoothingOff_ValidModel()
        {
            var model = _objReader.Read(@"Models\obj\SmoothingOff.obj");
            Assert.AreEqual(1, model.Children.Count);
            var gm1 = model.Children[0] as GeometryModel3D;
            Assert.NotNull(gm1);
            var mg1 = gm1.Geometry as MeshGeometry3D;
            Assert.NotNull(mg1);
            Assert.AreEqual(4, mg1.TriangleIndices.Count / 3);
        }

        [Test]
        public void Read_SmoothingGroup0_ValidModel()
        {
            var model = _objReader.Read(@"Models\obj\SmoothingGroup0.obj");
            Assert.AreEqual(1, model.Children.Count);
            var gm1 = model.Children[0] as GeometryModel3D;
            Assert.NotNull(gm1);
            var mg1 = gm1.Geometry as MeshGeometry3D;
            Assert.NotNull(mg1);
            Assert.AreEqual(4, mg1.TriangleIndices.Count / 3);
        }

        [Test]
        public void Read_SmoothingGroup1_ValidModel()
        {
            var model = _objReader.Read(@"Models\obj\SmoothingGroup1.obj");
            Assert.AreEqual(1, model.Children.Count);
            var gm1 = model.Children[0] as GeometryModel3D;
            Assert.NotNull(gm1);
            var mg1 = gm1.Geometry as MeshGeometry3D;
            Assert.NotNull(mg1);
            Assert.AreEqual(4, mg1.TriangleIndices.Count / 3);
        }

        [Test]
        public void CanParseSimpleTriangle() 
        {
            var model = _objReader.Read(@"Models\obj\simple_triangle.obj");

            Assert.AreEqual(1, model.Children.Count);
            model.Children[0].AssertHasVertices(new[] {-1d,0d,1d}, new[] {1d,0d,1d}, new[] {-1d,0d,-1d});
        }

        [Test]
        public void CanParseLineContinuations() 
        {
            var model = _objReader.Read(@"Models\obj\line_continuation_single.obj");

            Assert.AreEqual(1, model.Children.Count);
            model.Children[0].AssertHasVertices(new[] {-1d,0d,1d}, new[] {1d,0d,1d}, new[] {-1d,0d,-1d});
        }

        [Test]
        public void CanParseLineContinuationsWithMultipleBreaks() 
        {
            var model = _objReader.Read(@"Models\obj\line_continuation_multiple_breaks.obj");

            Assert.AreEqual(1, model.Children.Count);
            model.Children[0].AssertHasVertices(new[] {-1d,0d,1d}, new[] {1d,0d,1d}, new[] {-1d,0d,-1d});
        }

        [Test]
        public void CanParseLineContinuationsWithEmptyContinuations() 
        {
            var model = _objReader.Read(@"Models\obj\line_continuation_empty_continuation.obj");

            Assert.AreEqual(1, model.Children.Count);
            model.Children[0].AssertHasVertices(new[] {-1d,0d,1d}, new[] {1d,0d,1d}, new[] {-1d,0d,-1d});
        }

        [Test]
        public void CanParseLineContinuationsWithEmptyLineInMiddle() 
        {
            var model = _objReader.Read(@"Models\obj\line_continuation_empty_line.obj");

            Assert.AreEqual(1, model.Children.Count);
            model.Children[0].AssertHasVertices(new[] {-1d,0d,1d}, new[] {1d,0d,1d}, new[] {-1d,0d,-1d});
        }

        [Test]
        public void CanParseLineContinuationsInComments() 
        {
            var model = _objReader.Read(@"Models\obj\line_continuation_comment.obj");

            Assert.AreEqual(1, model.Children.Count);
            model.Children[0].AssertHasVertices(new[] {-1d,0d,1d}, new[] {1d,0d,1d}, new[] {-1d,0d,-1d});
        }
    }

    public static class Model3DTestExtensions 
    {
        public static void AssertHasVertices(this Model3D model, params double[][] vertices) 
        {
            var geometryModel = (GeometryModel3D) model;
            var geometry = (MeshGeometry3D) geometryModel.Geometry;
            Assert.AreEqual(vertices.Length, geometry.Positions.Count, "Expected to find {0} vertices in model", vertices.Length);
            foreach (var vertex in vertices)
                Assert.IsTrue(geometry.Positions.Contains(new Point3D(vertex[0], vertex[1], vertex[2])), "Expected geometry to contain vertex [{0},{1},{2}]", vertex[0],vertex[1],vertex[2]);
        }
    }
}