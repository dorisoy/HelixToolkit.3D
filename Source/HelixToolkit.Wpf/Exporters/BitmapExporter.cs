﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitmapExporter.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace HelixToolkit.Wpf
{
    using System;
    using System.IO;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Media.Media3D;

    /// <summary>
    /// Exports a <see cref="Viewport3D"/> to a .bmp, .png or .jpg file.
    /// </summary>
    public class BitmapExporter : IExporter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapExporter"/> class.
        /// </summary>
        public BitmapExporter()
        {
            this.OversamplingMultiplier = 2;
        }

        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        /// <value>The background.</value>
        public Brush Background { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the oversampling multiplier.
        /// </summary>
        /// <value>The oversampling multiplier.</value>
        public int OversamplingMultiplier { get; set; }

        /// <summary>
        /// Exports the specified viewport.
        /// </summary>
        /// <param name="viewport">The viewport.</param>
        /// <param name="stream">The output stream.</param>
        /// <exception cref="System.InvalidOperationException">Not supported file format.</exception>
        public void Export(Viewport3D viewport, Stream stream)
        {
            var background = this.Background ?? Brushes.Transparent;

            var bmp = viewport.RenderBitmap(background, this.OversamplingMultiplier);
            BitmapEncoder encoder;
            string ext = Path.GetExtension(this.FileName) ?? string.Empty;
            switch (ext.ToLower())
            {
                case ".jpg":
                    encoder = new JpegBitmapEncoder();
                    break;
                case ".bmp":
                    encoder = new BmpBitmapEncoder();
                    break;
                case ".png":
                    encoder = new PngBitmapEncoder();
                    break;
                default:
                    throw new InvalidOperationException("Not supported file format.");
            }

            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(stream);
        }

        /// <summary>
        /// Exports the specified visual.
        /// </summary>
        /// <param name="visual">The visual.</param>
        /// <param name="stream">The output stream.</param>
        /// <exception cref="System.NotImplementedException">Cannot export a visual to a bitmap.</exception>
        public void Export(Visual3D visual, Stream stream)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Exports the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="stream">The output stream.</param>
        /// <exception cref="System.NotImplementedException">Cannot export a model to a bitmap.</exception>
        public void Export(Model3D model, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}