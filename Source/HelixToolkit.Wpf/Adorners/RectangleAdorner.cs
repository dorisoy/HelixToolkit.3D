﻿using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using CommunityToolkit.Diagnostics;

namespace HelixToolkit.Wpf;

/// <summary>
/// An adorner showing a rectangle with a crosshair in the middle. This is shown when zooming a rectangle.
/// </summary>
public sealed class RectangleAdorner : Adorner
{
    /// <summary>
    /// The cross hair size.
    /// </summary>
    private readonly double crossHairSize;

    /// <summary>
    /// The pen.
    /// </summary>
    private readonly Pen? pen;

    /// <summary>
    /// The brush to color the inner rectangle
    /// </summary>
    private readonly Brush? fillBrush;

    /// <summary>
    /// The pen 2.
    /// </summary>
    private readonly Pen? pen2;

    /// <summary>
    /// Initializes a new instance of the <see cref="RectangleAdorner"/> class.
    /// </summary>
    /// <param name="adornedElement">
    /// The adorned element.
    /// </param>
    /// <param name="rectangle">
    /// The rectangle.
    /// </param>
    /// <param name="color1">
    /// The color1.
    /// </param>
    /// <param name="color2">
    /// The color2.
    /// </param>
    /// <param name="thickness1">
    /// The thickness1.
    /// </param>
    /// <param name="thickness2">
    /// The thickness2.
    /// </param>
    /// <param name="crossHairSize">
    /// Size of the cross hair.
    /// </param>
    ///<exception cref="ArgumentNullException">
    ///</exception>
    public RectangleAdorner(
        UIElement adornedElement,
        Rect rectangle,
        Color color1,
        Color color2,
        double thickness1 = 1.0,
        double thickness2 = 1.0,
        double crossHairSize = 10)
        : this(adornedElement, rectangle, color1, color2, thickness1, thickness2, crossHairSize, DashStyles.Dash, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RectangleAdorner"/> class.
    /// </summary>
    /// <param name="adornedElement">
    /// The adorned element.
    /// </param>
    /// <param name="rectangle">
    /// The rectangle.
    /// </param>
    /// <param name="color1">
    /// The color1.
    /// </param>
    /// <param name="color2">
    /// The color2.
    /// </param>
    /// <param name="thickness1">
    /// The thickness1.
    /// </param>
    /// <param name="thickness2">
    /// The thickness2.
    /// </param>
    /// <param name="crossHairSize">
    /// Size of the cross hair.
    /// </param>
    /// <param name="dashStyle2">
    /// The dash style2.
    /// </param>
    /// <param name="fillBrush">
    /// The brush to color the inner rectangle
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// </exception>
    public RectangleAdorner(
        UIElement adornedElement,
        Rect rectangle,
        Color color1,
        Color color2,
        double thickness1,
        double thickness2,
        double crossHairSize,
        DashStyle dashStyle2,
        Brush? fillBrush = null)
        : base(adornedElement)
    {
        Guard.IsNotNull(adornedElement);

        this.Rectangle = rectangle;

        // http://www.wpftutorial.net/DrawOnPhysicalDevicePixels.html
        var ps = PresentationSource.FromVisual(adornedElement);
        if (ps == null)
        {
            return;
        }

        var ct = ps.CompositionTarget;
        if (ct == null)
        {
            return;
        }

        var m = ct.TransformToDevice;
        double dpiFactor = 1 / m.M11;

        this.pen = new Pen(new SolidColorBrush(color1), thickness1 * dpiFactor);
        this.pen2 = new Pen(new SolidColorBrush(color2), thickness2 * dpiFactor);
        this.pen2.DashStyle = dashStyle2;
        this.crossHairSize = crossHairSize;
        this.fillBrush = fillBrush;
    }

    /// <summary>
    /// Gets or sets Rectangle.
    /// </summary>
    public Rect Rectangle { get; set; }

    /// <summary>
    /// Called when rendering.
    /// </summary>
    /// <param name="dc">
    /// The dc.
    /// </param>
    protected override void OnRender(DrawingContext dc)
    {
        if (this.pen is null || this.pen2 is null)
        {
            return;
        }

        double halfPenWidth = this.pen.Thickness / 2;

        double mx = (this.Rectangle.Left + this.Rectangle.Right) / 2;
        double my = (this.Rectangle.Top + this.Rectangle.Bottom) / 2;
        mx = (int)mx + halfPenWidth;
        my = (int)my + halfPenWidth;

        var rect = new Rect(
            (int)this.Rectangle.Left + halfPenWidth,
            (int)this.Rectangle.Top + halfPenWidth,
            (int)this.Rectangle.Width,
            (int)this.Rectangle.Height);

        // Create a guidelines set
        /*GuidelineSet guidelines = new GuidelineSet();
        guidelines.GuidelinesX.Add(rect.Left + halfPenWidth);
        guidelines.GuidelinesX.Add(rect.Right + halfPenWidth);
        guidelines.GuidelinesY.Add(rect.Top + halfPenWidth);
        guidelines.GuidelinesY.Add(rect.Bottom + halfPenWidth);
        guidelines.GuidelinesX.Add(mx + halfPenWidth);
        guidelines.GuidelinesY.Add(my + halfPenWidth);

        dc.PushGuidelineSet(guidelines);*/
        dc.DrawRectangle(this.fillBrush, this.pen, rect);
        dc.DrawRectangle(null, this.pen2, rect);

        if (this.crossHairSize > 0)
        {
            dc.DrawLine(this.pen, new Point(mx, my - this.crossHairSize), new Point(mx, my + this.crossHairSize));
            dc.DrawLine(this.pen, new Point(mx - this.crossHairSize, my), new Point(mx + this.crossHairSize, my));
            dc.DrawLine(this.pen2, new Point(mx, my - this.crossHairSize), new Point(mx, my + this.crossHairSize));
            dc.DrawLine(this.pen2, new Point(mx - this.crossHairSize, my), new Point(mx + this.crossHairSize, my));
        }

        // dc.Pop();
    }
}
