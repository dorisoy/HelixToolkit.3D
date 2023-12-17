﻿using HelixToolkit.SharpDX.Core2D;
using SharpDX;
using SharpDX.Direct2D1;

namespace HelixToolkit.SharpDX.Model.Scene2D;

public class BorderNode2D : ContentNode2D
{
    public float CornerRadius
    {
        set
        {
            if (RenderCore is BorderRenderCore2D core)
            {
                core.CornerRadius = value;
            }
        }
        get
        {
            if (RenderCore is BorderRenderCore2D core)
            {
                return core.CornerRadius;
            }

            return 0.0f;
        }
    }

    private Thickness padding = new(0);

    public Thickness Padding
    {
        set
        {
            SetAffectsMeasure(ref padding, value);
        }
        get
        {
            return padding;
        }
    }

    public Brush? BorderBrush
    {
        set
        {
            if (RenderCore is BorderRenderCore2D core)
            {
                core.StrokeBrush = value;
            }
        }
        get
        {
            if (RenderCore is BorderRenderCore2D core)
            {
                return core.StrokeBrush;
            }

            return null;
        }
    }

    private CapStyle strokeDashCap = CapStyle.Flat;

    public CapStyle StrokeDashCap
    {
        set
        {
            if (SetAffectsRender(ref strokeDashCap, value))
            {
                strokeStyleChanged = true;
            }
        }
        get
        {
            return strokeDashCap;
        }
    }

    private CapStyle strokeStartLineCap = CapStyle.Flat;

    public CapStyle StrokeStartLineCap
    {
        set
        {
            if (SetAffectsRender(ref strokeStartLineCap, value))
            {
                strokeStyleChanged = true;
            }
        }
        get
        {
            return strokeStartLineCap;
        }
    }

    private CapStyle strokeEndLineCap = CapStyle.Flat;

    public CapStyle StrokeEndLineCap
    {
        set
        {
            if (SetAffectsRender(ref strokeEndLineCap, value))
            {
                strokeStyleChanged = true;
            }
        }
        get
        {
            return strokeEndLineCap;
        }
    }

    private DashStyle strokeDashStyle = DashStyle.Solid;

    public DashStyle StrokeDashStyle
    {
        set
        {
            if (SetAffectsRender(ref strokeDashStyle, value))
            {
                strokeStyleChanged = true;
            }
        }
        get
        {
            return strokeDashStyle;
        }
    }

    private float strokeDashOffset = 0;

    public float StrokeDashOffset
    {
        set
        {
            if (SetAffectsRender(ref strokeDashOffset, value))
            {
                strokeStyleChanged = true;
            }
        }
        get
        {
            return strokeDashOffset;
        }
    }

    private LineJoin strokeLineJoin = LineJoin.Miter;

    public LineJoin StrokeLineJoin
    {
        set
        {
            if (SetAffectsRender(ref strokeLineJoin, value))
            {
                strokeStyleChanged = true;
            }
        }
        get
        {
            return strokeLineJoin;
        }
    }

    private float strokeMiterLimit = 1;

    public float StrokeMiterLimit
    {
        set
        {
            if (SetAffectsRender(ref strokeMiterLimit, value))
            {
                strokeStyleChanged = true;
            }
        }
        get
        {
            return strokeMiterLimit;
        }
    }

    private Thickness borderThickness;

    public Thickness BorderThickness
    {
        set
        {
            if (SetAffectsMeasure(ref borderThickness, value))
            {
                if (RenderCore is BorderRenderCore2D core)
                {
                    core.BorderThickness = value;
                }
            }
        }
        get
        {
            return borderThickness;
        }
    }

    private bool strokeStyleChanged = true;

    protected override bool OnAttach(IRenderHost host)
    {
        if (base.OnAttach(host))
        {
            strokeStyleChanged = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Update(RenderContext2D context)
    {
        base.Update(context);
        if (strokeStyleChanged)
        {
            if (RenderCore is BorderRenderCore2D core && context.DeviceResources is not null)
            {
                core.StrokeStyle = new StrokeStyle(context.DeviceResources.Factory2D,
                    new StrokeStyleProperties()
                    {
                        DashCap = StrokeDashCap,
                        StartCap = StrokeStartLineCap,
                        EndCap = StrokeEndLineCap,
                        DashOffset = StrokeDashOffset,
                        LineJoin = StrokeLineJoin,
                        MiterLimit = Math.Max(1, StrokeMiterLimit),
                        DashStyle = StrokeDashStyle
                    });
            }
            strokeStyleChanged = false;
        }
    }

    protected override Vector2 MeasureOverride(Vector2 availableSize)
    {
        if (Content != null)
        {
            var margin = new Vector2((BorderThickness.Left / 2 + Padding.Left + BorderThickness.Right / 2 + Padding.Right),
                (BorderThickness.Top / 2 + Padding.Top + BorderThickness.Bottom / 2 + Padding.Bottom));
            margin.X *= DpiScale;
            margin.Y *= DpiScale;
            var childAvail = new Vector2(Math.Max(0, availableSize.X - margin.X), Math.Max(0, availableSize.Y - margin.Y));

            var size = base.MeasureOverride(childAvail);
            if (Width != float.PositiveInfinity && Height != float.PositiveInfinity)
            {
                return availableSize;
            }
            else
            {
                if (Width != float.PositiveInfinity)
                {
                    size.X = Width * DpiScale;
                }
                if (Height != float.PositiveInfinity)
                {
                    size.Y = Height * DpiScale;
                }
                return size;
            }
        }
        else
        {
            var size = new Vector2((float)(BorderThickness.Left / 2 + Padding.Left + BorderThickness.Right / 2 + Padding.Right + MarginWidthHeight.X + Width == float.PositiveInfinity ? 0 : Width),
                (float)(BorderThickness.Top / 2 + Padding.Top + BorderThickness.Bottom / 2 + Padding.Bottom + MarginWidthHeight.Y) + Height == float.PositiveInfinity ? 0 : Height);
            size.X *= DpiScale;
            size.Y *= DpiScale;
            return size;
        }
    }

    protected override RectangleF ArrangeOverride(RectangleF finalSize)
    {
        var contentRect = new RectangleF(finalSize.Left, finalSize.Top, finalSize.Width, finalSize.Height);
        contentRect.Left += (float)(BorderThickness.Left / 2 + Padding.Left) * DpiScale;
        contentRect.Right -= (float)(BorderThickness.Right / 2 + Padding.Right) * DpiScale;
        contentRect.Top += (float)(BorderThickness.Top / 2 + Padding.Top) * DpiScale;
        contentRect.Bottom -= (float)(BorderThickness.Bottom / 2 + Padding.Bottom) * DpiScale;
        base.ArrangeOverride(contentRect);
        return finalSize;
    }
}
