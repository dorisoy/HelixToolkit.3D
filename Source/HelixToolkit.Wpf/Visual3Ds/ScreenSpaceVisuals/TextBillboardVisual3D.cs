﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBillboardVisual3D.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: MIT
// </copyright>
// <summary>
//   A visual element that contains a text billboard.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HelixToolkit.Wpf
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Media.Media3D;

    /// <summary>
    /// A visual element that contains a text billboard.
    /// </summary>
    public class TextBillboardVisual3D : BillboardVisual3D
    {
        /// <summary>
        /// The background property
        /// </summary>
        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(
            "Background", typeof(Brush), typeof(TextBillboardVisual3D), new UIPropertyMetadata(null, VisualChanged));

        /// <summary>
        /// The border brush property
        /// </summary>
        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(
            "BorderBrush", typeof(Brush), typeof(TextBillboardVisual3D), new UIPropertyMetadata(null, VisualChanged));

        /// <summary>
        /// The border thickness property
        /// </summary>
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(
                "BorderThickness",
                typeof(Thickness),
                typeof(TextBillboardVisual3D),
                new UIPropertyMetadata(new Thickness(1), VisualChanged));

        /// <summary>
        /// The font family property.
        /// </summary>
        public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register(
            "FontFamily", typeof(FontFamily), typeof(TextBillboardVisual3D), new UIPropertyMetadata(null, VisualChanged));

        /// <summary>
        /// The font size property.
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            "FontSize", typeof(double), typeof(TextBillboardVisual3D), new UIPropertyMetadata(0.0, VisualChanged));

        /// <summary>
        /// The font weight property.
        /// </summary>
        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register(
            "FontWeight",
            typeof(FontWeight),
            typeof(TextBillboardVisual3D),
            new UIPropertyMetadata(FontWeights.Normal, VisualChanged));

        /// <summary>
        /// The foreground property.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
            "Foreground",
            typeof(Brush),
            typeof(TextBillboardVisual3D),
            new UIPropertyMetadata(Brushes.Black, VisualChanged));

        /// <summary>
        /// The height factor property
        /// </summary>
        public static readonly DependencyProperty HeightFactorProperty = DependencyProperty.Register(
            "HeightFactor", typeof(double), typeof(TextBillboardVisual3D), new PropertyMetadata(1.0, VisualChanged));

        /// <summary>
        /// The padding property
        /// </summary>
        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            "Padding",
            typeof(Thickness),
            typeof(TextBillboardVisual3D),
            new UIPropertyMetadata(new Thickness(0), VisualChanged));

        /// <summary>
        /// The text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(TextBillboardVisual3D), new UIPropertyMetadata(null, VisualChanged));

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public Brush Background
        {
            get
            {
                return (Brush)this.GetValue(BackgroundProperty);
            }
            set
            {
                this.SetValue(BackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the border brush.
        /// </summary>
        /// <value>The border brush.</value>
        public Brush BorderBrush
        {
            get
            {
                return (Brush)this.GetValue(BorderBrushProperty);
            }
            set
            {
                this.SetValue(BorderBrushProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        /// <value>The border thickness.</value>
        public Thickness BorderThickness
        {
            get
            {
                return (Thickness)this.GetValue(BorderThicknessProperty);
            }
            set
            {
                this.SetValue(BorderThicknessProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        /// <value>The font family.</value>
        public FontFamily FontFamily
        {
            get
            {
                return (FontFamily)this.GetValue(FontFamilyProperty);
            }

            set
            {
                this.SetValue(FontFamilyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        public double FontSize
        {
            get
            {
                return (double)this.GetValue(FontSizeProperty);
            }

            set
            {
                this.SetValue(FontSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the font weight.
        /// </summary>
        /// <value>The font weight.</value>
        public FontWeight FontWeight
        {
            get
            {
                return (FontWeight)this.GetValue(FontWeightProperty);
            }

            set
            {
                this.SetValue(FontWeightProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the foreground brush.
        /// </summary>
        /// <value>The foreground.</value>
        public Brush Foreground
        {
            get
            {
                return (Brush)this.GetValue(ForegroundProperty);
            }

            set
            {
                this.SetValue(ForegroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the height factor.
        /// </summary>
        /// <value>
        /// The height factor.
        /// </value>
        public double HeightFactor
        {
            get
            {
                return (double)this.GetValue(HeightFactorProperty);
            }

            set
            {
                this.SetValue(HeightFactorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        /// <value>The padding.</value>
        public Thickness Padding
        {
            get
            {
                return (Thickness)this.GetValue(PaddingProperty);
            }
            set
            {
                this.SetValue(PaddingProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }

            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// The visual appearance changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void VisualChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TextBillboardVisual3D)d).VisualChanged();
        }

        /// <summary>
        /// Updates the text block when the visual appearance changed.
        /// </summary>
        private void VisualChanged()
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Material = null;
                return;
            }

            var textBlock = new TextBlock(new Run(this.Text))
                                {
                                    Foreground = this.Foreground,
                                    Background = this.Background,
                                    FontWeight = this.FontWeight,
                                    Padding = this.Padding
                                };

            if (this.FontFamily != null)
            {
                textBlock.FontFamily = this.FontFamily;
            }

            if (this.FontSize > 0)
            {
                textBlock.FontSize = this.FontSize;
            }

            var element = this.BorderBrush != null
                              ? (FrameworkElement)
                                new Border
                                    {
                                        BorderBrush = this.BorderBrush,
                                        BorderThickness = this.BorderThickness,
                                        Child = textBlock
                                    }
                              : textBlock;

            element.Measure(new Size(1000, 1000));
            element.Arrange(new Rect(element.DesiredSize));

            var rtb = new RenderTargetBitmap(
                (int)element.ActualWidth + 1, (int)element.ActualHeight + 1, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(element);
            this.Material = new DiffuseMaterial(new ImageBrush(rtb));
            this.Width = element.ActualWidth;
            this.Height = element.ActualHeight * this.HeightFactor;
        }
    }
}